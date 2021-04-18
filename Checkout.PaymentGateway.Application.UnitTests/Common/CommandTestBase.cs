using Checkout.PaymentGateway.Infrastructure.DatabaseFactory;
using System;

namespace Checkout.PaymentGateway.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly CheckoutWebDbContext _context;

        public CommandTestBase()
        {
            _context = CheckoutWebDbContextFactory.Create();
        }

        public void Dispose()
        {
            CheckoutWebDbContextFactory.Destroy(_context);
        }
    }
}
