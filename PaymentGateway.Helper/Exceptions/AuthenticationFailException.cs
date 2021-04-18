using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Helper.Exceptions
{
    public class AuthenticationFailException : Exception
    {
        public AuthenticationFailException(string merchantId, object apikey)
             : base($"Merchant with  merchantID : \"{merchantId}\" and  ({apikey}) was not found.")
        {
        }
    }
}
