using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Helper.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string name, object key)
             : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
