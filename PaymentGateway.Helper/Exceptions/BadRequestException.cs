using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Helper.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string name, object message)
             : base($"Bad request on  {name} with error :  ({message}) ")
        {
        }
    }
}
