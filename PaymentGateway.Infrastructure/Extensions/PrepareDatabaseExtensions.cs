using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Infrastructure.DatabaseFactory;
using System;
using System.Linq;

namespace Checkout.PaymentGateway.Infrastructure.Extensions
{
    public static class PrepareDatabaseExtensions
    {
        public static void PrepareDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CheckoutWebDbContext>());
            }
        }

        public static void SeedData(CheckoutWebDbContext context)
        {
            System.Console.WriteLine("Migration started...");

            context.Database.Migrate();

            if (!context.Merchants.Any())
            {
                System.Console.WriteLine("Adding Test Merchants.. - seeding...");
                context.Merchants.AddRange(
                    new Merchant()
                    {
                        MerchantID = "HB123H7123G712",
                        ApiKey = "314179fa-7de9-4c9d-8d52-fb6f62ab3815",
                        CreatedDateTime = DateTime.Now,
                        LastUpdatedDateTime = DateTime.Now
                    },
                    new Merchant()
                    {
                        MerchantID = "HB123H7123G712",
                        ApiKey = "14ec3398-45aa-4149-82c7-d9c6fa594bcf",
                        CreatedDateTime = DateTime.Now,
                        LastUpdatedDateTime = DateTime.Now
                    },
                    new Merchant()
                    {
                        MerchantID = "K12312N21M123",
                        ApiKey = "b93b78f9-fb2f-4a00-9c55-95bb4ae2fc6a",
                        CreatedDateTime = DateTime.Now,
                        LastUpdatedDateTime = DateTime.Now
                    },
                    new Merchant()
                    {
                        MerchantID = "1231MN11H2781",
                        ApiKey = "352015a8-7553-40d1-8870-8f382a1256ae",
                        CreatedDateTime = DateTime.Now,
                        LastUpdatedDateTime = DateTime.Now
                    });

                context.SaveChanges();

                System.Console.WriteLine("Data added successfully.");
            }
        }
    }
}
