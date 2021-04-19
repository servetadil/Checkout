using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Checkout.PaymentGateway.Domain.SharedKernel
{
    public class Money : ValueObject<Money>
    {
        [Required]
        public decimal Value { get; set; }

        [Required]
        public string Currency { get; set; }

        public Money()
        {
        }

        public Money(decimal value, string currencyCode)
        {
            if (value <= 0 || currencyCode == null)
                throw new ArgumentNullException("Cannot create  0 Amount or without currencyCode");

            Value = value;
            Currency = currencyCode;
        }
    }
}
