using Checkout.PaymentGateway.Domain.Common;

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
