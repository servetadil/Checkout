using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Infrastructure.DatabaseFactory;
using Checkout.PaymentGateway.Infrastructure.Repositories;

namespace Checkout.PaymentGateway.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<CheckoutWebDbContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:DefaultConnection")
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
