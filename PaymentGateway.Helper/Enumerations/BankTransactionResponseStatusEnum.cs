using Checkout.PaymentGateway.Helper.Common;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Helper.Enums
{
    public class BankTransactionResponseStatusEnum : Enumeration
    {
        public static BankTransactionResponseStatusEnum PaymentSucceeded = new BankTransactionResponseStatusEnum(201, "Payment Success");
        public static BankTransactionResponseStatusEnum PaymentFailed = new BankTransactionResponseStatusEnum(400, "Payment Failed");
        public static BankTransactionResponseStatusEnum FuturePaymentSubmitted = new BankTransactionResponseStatusEnum(202, "Future Payment Submitted");

        protected BankTransactionResponseStatusEnum() { }

        public BankTransactionResponseStatusEnum(int id, string name)
            : base(id, name)
        {
        }
    }
}
