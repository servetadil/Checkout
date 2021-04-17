using Microsoft.EntityFrameworkCore;
using PaymentGateway.Infrastructure.DatabaseFactory;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.Infrastructure.IntegrationTests
{
    public class CheckoutWebDbContextTests : IDisposable
    {
        private readonly CheckoutWebDbContext _sut;
        public CheckoutWebDbContextTests()
        {
            var options = new DbContextOptionsBuilder<CheckoutWebDbContext>()
                  .Options;

            _sut = new CheckoutWebDbContext(options);
        }

        [Fact]
        public void Test1()
        {

        }

        public void Dispose()
        {
            _sut?.Dispose();
        }
    }
}
