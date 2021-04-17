using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Common
{
    public abstract class ValueObject<T>
           where T : ValueObject<T>
    {

    }
}
