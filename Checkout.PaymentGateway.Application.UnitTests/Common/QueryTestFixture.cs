using AutoMapper;
using Checkout.PaymentGateway.Application.Mappers;
using Checkout.PaymentGateway.Helper.Common;
using Checkout.PaymentGateway.Helper.Encryption;
using System;
using Xunit;

namespace Checkout.PaymentGateway.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public Infrastructure.DatabaseFactory.CheckoutWebDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public ApplicationSettings AppSettings { get; private set; }

        public IEncryptionService EncryptionService { get; private set; }

        public QueryTestFixture()
        {
            Context = CheckoutWebDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MerchantsMapperProfile>();
            });

            Mapper = configurationProvider.CreateMapper();

            AppSettings = new ApplicationSettings() { JwtKey = "TesTesttTestTest", DesEncrytpKey = "TestTestTestTest", JwtIssuer = "TestTestTestTest" };

            EncryptionService = new EncryptionService();
        }

        public void Dispose()
        {
            CheckoutWebDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
