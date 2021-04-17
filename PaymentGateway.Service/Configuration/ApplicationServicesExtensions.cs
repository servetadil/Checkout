using Checkout.PaymentGateway.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Checkout.PaymentGateway.Application.Configuration
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(ICrudService<>), typeof(CrudService<>))
                .AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
