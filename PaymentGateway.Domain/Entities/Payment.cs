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
        public Merchant Merchant { get; set; }

        [Required]
        public Money Amount { get; set; }

        public CreditCard Card { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required]
        public bool IsFutureTransaction { get; set; }

        public int PaymentStatus { get; set; }

        public Payment()
        {

        }
    }
}
