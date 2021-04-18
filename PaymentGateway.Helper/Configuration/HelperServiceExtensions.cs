using Checkout.PaymentGateway.Helper.Encryption;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.PaymentGateway.Helper.Configuration
{
    public static class HelperServiceExtensions
    {
        public static IServiceCollection AddHelperServices(this IServiceCollection services)
        {
            services.AddTransient<IEncryptionService, EncryptionService>();
            return services;
        }
    }
}