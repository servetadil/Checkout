using Bank.PaymentProcessor.Model;
using Bank.PaymentProcessor.PaymentProcessor;
using Checkout.PaymentGateway.Application.Common;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using Checkout.PaymentGateway.Application.Payments.Service;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Domain.SharedKernel;
using Checkout.PaymentGateway.Helper.Encryption;
using Checkout.PaymentGateway.Helper.Enums;
using Checkout.PaymentGateway.Helper.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Commands.SubmitPayment
{
    public class SubmitPaymentCommandHandler : IRequestHandler<SubmitPaymentCommand, SubmitPaymentResultWm>
    {
        private readonly IPaymentService _paymentService;
        private readonly IEncryptionService _encryptionService;
        private readonly IAuthorizationService _authService;
        private readonly IPaymentProcessor _mockBankClient;

        public SubmitPaymentCommandHandler(
            IPaymentService paymentService,
            IEncryptionService encryptionService,
            IAuthorizationService authService,
            IPaymentProcessor mockBankClient)
        {
            _paymentService = paymentService;
            _encryptionService = encryptionService;
            _authService = authService;
            _mockBankClient = mockBankClient;

        }

        public async Task<SubmitPaymentResultWm> Handle(SubmitPaymentCommand request, CancellationToken cancellationToken)
        {
            var authenticatedUser = _authService.GetAuthenticatedMerchant();

            var payment = await _paymentService.GetPaymentByPaymentID(request.PaymentID, authenticatedUser.MerchantID, authenticatedUser.ApiKey);

            if (payment == null)
                throw new NotFoundException(nameof(Merchant), request.PaymentID.ToString());

            try
            {
                payment.PaymentStatus = PaymentProcessEnum.RequestPayment.Id;
                payment.Card = new CreditCard()
                {
                    CardName = _encryptionService.Encrypt(request.CardName),
                    CardNumber = _encryptionService.Encrypt(request.CardNumber),
                    CvvCode = _encryptionService.Encrypt(request.CvvCode.ToString()),
                    ExpiryMonth = _encryptionService.Encrypt(request.ExpiryMonth.ToString()),
                    ExpiryYear = _encryptionService.Encrypt(request.ExpiryYear.ToString())
                };

                payment.LastUpdatedDateTime = DateTime.Now;
                payment.IsFutureTransaction = false;

                var bankResponse = await _mockBankClient.CreatePaymentTransactionAsync(
                    new Uri("https://localhost:44306"),
                    new TransactionRequest
                    {
                        CardMonth = request.ExpiryMonth,
                        CardName = request.CardName,
                        CardNumber = request.CardNumber,
                        CardYear = request.ExpiryYear,
                        Cvv = request.CvvCode,
                        PaymentTrackID = payment.OrderID
                    });

                if (bankResponse.StatusCode == BankTransactionResponseStatusEnum.PaymentSucceeded.Id)
                {
                    payment.PaymentStatus = PaymentProcessEnum.RequestPayment.Id;
                }

                await _paymentService.Update(payment);

                return new SubmitPaymentResultWm()
                {
                    OrderID = payment.OrderID,
                    ResponseCode = PaymentProcessEnum.PaymentSucceeded.Id,
                    ResponseMessage = PaymentProcessEnum.PaymentSucceeded.Name

                };
            }
            catch (Exception ex)
            {
                payment.PaymentStatus = PaymentProcessEnum.PaymentFailed.Id;
                await _paymentService.Update(payment);
                throw new BadRequestException(nameof(SubmitPaymentCommandHandler), "Payment failed");
            }
        }
    }
}