using Checkout.PaymentGateway.Application.Common;
using Checkout.PaymentGateway.Application.Payments.Service;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IPaymentService _paymentService;
        private readonly IAuthorizationService _authService;

        public CreatePaymentCommandHandler(
            
            IPaymentService paymentService,
            IAuthorizationService authService)
        {
            _paymentService = paymentService;
            _authService = authService;
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentId = Guid.NewGuid();
            var commandUser = _authService.GetAuthenticatedMerchant();

            var payment = new Payment(
                paymentId,
                request.OrderID,
                request.Amount,
                request.Currency,
                commandUser.MerchantID,
                commandUser.ApiKey,
                PaymentProcessEnum.CreatePayment.Id);

            await _paymentService.Create(payment);

            return paymentId;
        }
    }
}