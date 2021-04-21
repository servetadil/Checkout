using Checkout.PaymentGateway.Domain.SharedKernel;
using System;

namespace Checkout.PaymentGateway.Application.Payments.Queries.GetPaymenDetail
{
    public class GetPaymentDetailVm
    {
        public Guid PaymentID { get; set; }

        public string OrderID { get; set; }

        public string BankTransactionID { get; set; }

        public Money Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsFutureTransaction { get; set; }

        public int PaymentStatusCode { get; set; }

        public string CardNumber { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
    }
}
