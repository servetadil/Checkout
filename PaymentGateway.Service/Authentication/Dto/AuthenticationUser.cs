using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Checkout.PaymentGateway.Application.Authentication.User
{
    public class AuthenticationUser
    {
        public string MerchantID { get; set; }

        public string ApiKey { get; set; }

        public string Secret { get; set; }
    }
}
