using System;

namespace Checkout.PaymentGateway.Helper.Exceptions
{
    public class SecurityTokenExpiredException : Exception
    {
        public SecurityTokenExpiredException()
             : base($"Auth token has been expired or not generated.")
        {

        }
    }
}
