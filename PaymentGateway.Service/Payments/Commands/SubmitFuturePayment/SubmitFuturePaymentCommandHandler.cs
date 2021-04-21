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

namespace Checkout.PaymentGateway.Application.Payments.Commands.SubmitFuturePayment
{
    public class SubmitFuturePaymentCommandHandler : IRequestHandler<SubmitFuturePaymentCommand, SubmitFuturePaymentResultWm>
    {
        private readonly IPaymentService _paymentService;
        private readonly IEncryptionService _encryptionService;

        public SubmitFuturePaymentCommandHandler(
            IPaymentService paymentService,
            IEncryptionService encryptionService)
        {
            _paymentService = paymentService;
            _encryptionService = encryptionService;
        }

        public async Task<SubmitFuturePaymentResultWm> Handle(SubmitFuturePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await ValidateAndGetGeneratedPayment(request);

            try
            {
                await SaveRequestPreQuee(payment, request);

                // */*/* Optional TODO */*/*/*/*
                // 
                // Future Payment Command Is optional. ( For merchants who wants to process their membership etc. payments )
                // Here we can create new quee etc. and proccess future payments.
                //
                // For now it's just processing request and return as "Future payment" triggered.
                //
                // */*/*/*/*/*/*/*/*/*/

                return new SubmitFuturePaymentResultWm()
                {
                    OrderID = payment.OrderID,
                    ResponseCode = PaymentProcessEnum.RequestFuturePayment.Id,
                    ResponseMessage = PaymentProcessEnum.RequestFuturePayment.Name
                };
            }
            catch (Exception)
            {
                payment.PaymentStatus = PaymentProcessEnum.RequestFuturePaymentFail.Id;
                await _paymentService.Update(payment);
                throw new BadRequestException(nameof(SubmitFuturePaymentCommandHandler), "Payment failed");
            }
        }

        private async Task<Payment> ValidateAndGetGeneratedPayment(SubmitFuturePaymentCommand request)
        {
            var payment = await _paymentService.GetPaymentByPaymentID(request.PaymentID);

            if (payment == null)
                throw new NotFoundException(nameof(Merchant), request.PaymentID.ToString());

            return payment;
        }

        private async Task<Payment> SaveRequestPreQuee(Payment payment, SubmitFuturePaymentCommand request)
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

            payment.PaymentDate = DateTime.Now;
            payment.IsFutureTransaction = true;
            await _paymentService.Update(payment);

            return payment;
        }
    }
}