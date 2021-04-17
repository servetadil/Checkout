using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Domain.Common
{
    public abstract class ValueObject<T>
           where T : ValueObject<T>
    {

    }
}
