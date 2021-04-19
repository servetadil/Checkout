using Checkout.PaymentGateway.Application.Authentication.User;

namespace Checkout.PaymentGateway.Application.Common
{
    public interface IAuthorizationService
    {
        AuthenticationUser GetAuthenticatedMerchant();
    }
}
