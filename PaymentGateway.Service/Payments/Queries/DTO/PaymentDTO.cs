using Checkout.PaymentGateway.Domain.SharedKernel;
using System;

namespace Checkout.PaymentGateway.Application.Payments.Dto
{
    public class PaymentDTO
    {
        public Guid PaymentID { get; set; }

        public string OrderID { get; set; }

        public Money Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsFutureTransaction { get; set; }

        public int PaymentStatusCode { get; set; }
    }
}
