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

            return context;
        }

        public static void Destroy(CheckoutWebDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}