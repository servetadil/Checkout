using MediatR;
using System;

namespace Checkout.PaymentGateway.Application.Payments.Commands.SubmitFuturePayment
{
    public class SubmitFuturePaymentCommand : IRequest<SubmitFuturePaymentResultWm>
    {
        public Guid PaymentID { get; set; }

        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string CvvCode { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}
