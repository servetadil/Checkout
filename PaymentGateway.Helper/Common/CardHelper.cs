using System;

namespace Checkout.PaymentGateway.Helper.Common
{
    public class CardHelper
    {
        public static string MaskCardNumber(string number)
        {
            return number.Substring(number.Length - 4)
                .PadLeft(number.Length, '*');
        }
    }
}
