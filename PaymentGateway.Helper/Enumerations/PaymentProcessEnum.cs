using Checkout.PaymentGateway.Helper.Common;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Helper.Enums
{
    public class PaymentProcessEnum : Enumeration
    {
        public static PaymentProcessEnum CreatePayment = new PaymentProcessEnum(100, "Debt Created");
        public static PaymentProcessEnum RequestPayment = new PaymentProcessEnum(203, "Payment Requested");
        public static PaymentProcessEnum RequestFuturePayment = new PaymentProcessEnum(202, "Future Payment Requested");
        public static PaymentProcessEnum PaymentSucceeded = new PaymentProcessEnum(201, "Payment Succeeded");
        public static PaymentProcessEnum PaymentFailed = new PaymentProcessEnum(400, "Payment Failed");
        public static PaymentProcessEnum RequestFuturePaymentFail = new PaymentProcessEnum(204, "Future Payment Request failed");
        protected PaymentProcessEnum() { }

        public PaymentProcessEnum(int id, string name)
            : base(id, name)
        {
        }
    }
}
