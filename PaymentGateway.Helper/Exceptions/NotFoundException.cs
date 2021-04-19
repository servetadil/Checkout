using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Helper.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
             : base($"Entity {name} - ({key}) was not found.")
        {
        } 

        public NotFoundException(string name)
            : base($"Entity with name {name}  was not found.")
        {
        }
    }
}
