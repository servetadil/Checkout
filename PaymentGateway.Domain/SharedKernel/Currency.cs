using PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.SharedKernel
{
    public class Currency : ValueObject<Currency>
    {
        public string Code { get; set; }

        public Currency(string code)
        {
            SetCode(code);
        }

        public void SetCode(string code)
           => Code = code ?? throw new ArgumentNullException(nameof(code));

    }
}
