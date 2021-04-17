using PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.SharedKernel
{
    public class Money : ValueObject<Money>
    {
        public decimal Value { get; set; }

        public Currency Currency { get; set; }

        public Money(decimal value, string currencyCode)
        {
            Value = value;
            SetCurrency(currencyCode);
        }

        public void SetCurrency(string currencyCode)
        {
            if (currencyCode == null)
                throw new ArgumentNullException(nameof(currencyCode));
            Currency = new Currency(currencyCode);
        }
    }
}
