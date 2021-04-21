using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Entities;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Authentication.Service
{
    public interface IMerchantService : ICrudService<Merchant>
    {
        Task<AuthenticationUser> GetMerchant(string merchantId, string apiKey);

        Task<AuthenticationUser> AuthenticateAndGenerateApiSecret(string merchantId, string apiKey);

        string GenerateJwtToken(string merchantId, string apiKey);
    }
}
