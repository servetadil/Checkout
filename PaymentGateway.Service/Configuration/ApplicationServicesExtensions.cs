using Checkout.PaymentGateway.Application.Authentication.Command;
using Checkout.PaymentGateway.Application.Authentication.Service;
using Checkout.PaymentGateway.Application.Behaviours;
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
                .AddScoped<IMerchantService, MerchantService>()
                .AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }

        public static IServiceCollection AddServiceHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreatePaymentCommand, Guid>, CreatePaymentCommandHandler>();
            services.AddScoped<IRequestHandler<AuthenticateUserCommand, string>, AuthenticateUserCommandHandler>();
            return services;
        }

        public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfiguration.Initialize());
            return services;
        }
    }
}
