using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Helper.Common
{
    public sealed class ApplicationSettings
    {
        public string DesEncrytpKey { get; set; }

        public string JwtKey { get; set; }

        public string JwtIssuer { get; set; }
    }
}
