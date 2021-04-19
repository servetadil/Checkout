using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Checkout.PaymentGateway.Domain.SharedKernel
{
    public class CreditCard : ValueObject<CreditCard>
    {
        public string CardNumber { get; set; }
         
        public string CvvCode { get; set; }

        public string CardName { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }

        public string CardNetwork { get; set; }
    }
}
