using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Infrastructure.DatabaseFactory;
using Microsoft.EntityFrameworkCore;
using System;

namespace Checkout.PaymentGateway.Application.UnitTests.Common
{
    public class CheckoutWebDbContextFactory
    {
        public static CheckoutWebDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CheckoutWebDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;

            var context = new CheckoutWebDbContext(options);

            context.Database.EnsureCreated();

            context.Merchants.AddRange(new[] {
                new Merchant 
                { 
                    MerchantID = "HB123H7123G712", 
                    ApiKey = "314179fa-7de9-4c9d-8d52-fb6f62ab3815",
                    CreatedDateTime=DateTime.Now,
                    LastUpdatedDateTime=DateTime.Now 
                }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(CheckoutWebDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}