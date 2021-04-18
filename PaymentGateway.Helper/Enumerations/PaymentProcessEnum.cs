using Checkout.PaymentGateway.Helper.Common;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Helper.Enums
{
    public class PaymentProcessEnum : Enumeration
    {
        public static PaymentProcessEnum CreatePayment = new PaymentProcessEnum(1, "Debt Created");
        public static PaymentProcessEnum RequestPayment = new PaymentProcessEnum(2, "Payment Requested");
        public static PaymentProcessEnum RequestFuturePayment = new PaymentProcessEnum(3, "Future Payment Requested");
        public static PaymentProcessEnum PaymentSucceeded = new PaymentProcessEnum(4, "Payment Succeeded");
        public static PaymentProcessEnum PaymentFailed = new PaymentProcessEnum(5, "PaymentFailed");

        protected PaymentProcessEnum() { }

        public PaymentProcessEnum(int id, string name)
            : base(id, name)
        {
        }
    }
}
