using Bank.PaymentProcessor.PaymentProcessor;
using Checkout.PaymentGateway.Application.Authentication.Command;
using Checkout.PaymentGateway.Application.Authentication.Service;
using Checkout.PaymentGateway.Application.Behaviours;
using Checkout.PaymentGateway.Application.Common;
using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using Checkout.PaymentGateway.Application.Payments.Commands.SubmitPayment;
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
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            return services;
        }

        public static IServiceCollection AddServiceHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreatePaymentCommand, Guid>, CreatePaymentCommandHandler>();
            services.AddScoped<IRequestHandler<AuthenticateUserCommand, string>, AuthenticateUserCommandHandler>();
            services.AddScoped<IRequestHandler<SubmitPaymentCommand, SubmitPaymentResultWm>, SubmitPaymentCommandHandler>();
            return services;
        }

        public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfiguration.Initialize());
            return services;
        }
    }
}
