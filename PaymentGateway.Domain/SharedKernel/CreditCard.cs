using PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.SharedKernel
{
    public class CreditCard : ValueObject<CreditCard>
    {
        public long CardNumber { get; set; }

        public int CvvCode { get; set; }

        public string CardName { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string CardNetwork { get; set; }
    }
}
