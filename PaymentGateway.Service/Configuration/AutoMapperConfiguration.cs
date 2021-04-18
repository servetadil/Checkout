using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Application.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(config =>
            {
                //config.AddProfile(new PaymentMapperProfile());
                config.AllowNullCollections = true;
            })
            .CreateMapper();
        }
    }
}
