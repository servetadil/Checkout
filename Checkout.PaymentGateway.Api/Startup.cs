using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Checkout.PaymentGateway.Infrastructure.Extensions;
using Checkout.PaymentGateway.Application.Configuration;
using MediatR;
using Checkout.PaymentGateway.Api.Middleware;
using Checkout.PaymentGateway.Api.Extensions;
using Checkout.PaymentGateway.Api.Helpers;

namespace Checkout.PaymentGateway.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddControllers();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Initialize Dbcontext
            services.AddPersistence();

            // Initialize Application Services
            services.AddApplicationServices();

            // Initialize Application Handlers
            services.AddServiceHandlers();

            // Initialize Automapper profiles
            services.AddAutoMapperConfigurations();

            // Initialize Swagger
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            // SwaggerUI
            app.UseSwaggerUI();

            // Feed Db
            PrepareDatabaseExtensions.PrepareDatabase(app);

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
