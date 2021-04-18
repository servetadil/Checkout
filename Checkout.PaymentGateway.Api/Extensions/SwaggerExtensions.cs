using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Checkout.PaymentGateway.Api.Extensions
{
    public static class SwaggerExtensions
    {
        private static readonly string ProductVersion = "v1";

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;
                options.SwaggerDoc(ProductVersion, new OpenApiInfo
                {
                    Title = "Checkout Payment Gateway API",
                    Version = ProductVersion
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ProductVersion}/swagger.json", ProductVersion);
                c.RoutePrefix = "api";
            });
        }
    }
}
