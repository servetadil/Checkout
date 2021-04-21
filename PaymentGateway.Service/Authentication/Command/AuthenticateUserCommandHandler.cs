using Checkout.PaymentGateway.Application.Authentication.Service;
using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Helper.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Authentication.Command
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticationUser>
    {
        private readonly IMerchantService _merchantService;

        public AuthenticateUserCommandHandler(IMerchantService merchantService)
        {
            _merchantService = merchantService;
        }

        public async Task<AuthenticationUser> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var authenticationUser = await _merchantService.AuthenticateAndGenerateApiSecret(request.MerchantID, request.ApiKey);

            if (authenticationUser == null)
                throw new AuthenticationFailException(request.MerchantID, request.ApiKey);


            return authenticationUser;
        }
    }
}