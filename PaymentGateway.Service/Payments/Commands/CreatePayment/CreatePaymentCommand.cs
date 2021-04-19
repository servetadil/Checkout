using MediatR;
using System;

namespace Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<Guid>
    {
        public string OrderID { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
