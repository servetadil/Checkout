using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Application.Authentication.Command
{

    public class AuthenticateUserCommand : IRequest<string>
    {
        public string MerchantID { get; set; }

        public string ApiKey { get; set; }
    }
}
