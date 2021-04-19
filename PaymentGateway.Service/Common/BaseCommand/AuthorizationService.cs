using AutoMapper;
using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Helper.Exceptions;
using Microsoft.AspNetCore.Http;
namespace Checkout.PaymentGateway.Application.Common
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public AuthenticationUser GetAuthenticatedMerchant()
        {
            if (_httpContextAccessor.HttpContext.Items["User"] == null)
                throw new SecurityTokenExpiredException();

            var contextUser = _httpContextAccessor.HttpContext.Items["User"];

            return _mapper.Map<AuthenticationUser>(contextUser);
        }
    }
}
