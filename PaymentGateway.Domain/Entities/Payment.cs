using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Checkout.PaymentGateway.Domain.Entities
{
    public class Payment : Entity
    {
        [Required]
        public Guid PaymentID { get; set; }

        [Required]
        public string OrderID { get; set; }

        [Required]
        public string MerchantID { get; set; }

        [Required]
        public string ApiKey { get; set; }

        public string BankTransactionID { get; set; }

        [Required]
        public Money Amount { get; set; }

        public CreditCard Card { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsFutureTransaction { get; set; }

        public int PaymentStatus { get; set; }

        public Payment()
        {

        }

        public Payment(
            Guid paymentId,
            string orderId,
            decimal amount,
            string currency,
            string merchantId,
            string apiKey,
            int paymentStatus)
        {
            PaymentID = paymentId;
            OrderID = orderId;
            Amount = new Money(amount, currency);
            MerchantID = merchantId;
            ApiKey = apiKey;
            PaymentStatus = paymentStatus;
        }
    }
}
