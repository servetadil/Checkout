using AutoMapper;
using Checkout.PaymentGateway.Application.Mappers;

namespace Checkout.PaymentGateway.Application.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile(new MerchantsMapperProfile());
                config.AddProfile(new PaymentsMapperProfile());
                config.AllowNullCollections = true;
            })
            .CreateMapper();
        }
    }
}
