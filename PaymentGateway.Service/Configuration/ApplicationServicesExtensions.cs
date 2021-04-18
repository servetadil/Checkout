using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using Checkout.PaymentGateway.Application.Payments.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Checkout.PaymentGateway.Application.Configuration
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(ICrudService<>), typeof(CrudService<>))
                .AddScoped<IPaymentService, PaymentService>()
                .AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddServiceHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreatePaymentCommand, Guid>, CreatePaymentCommandHandler>();
            return services;
        }

        public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfiguration.Initialize());
            return services;
        }
    }
}
