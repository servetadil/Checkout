using AutoMapper;
using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Domain.Entities;

namespace Checkout.PaymentGateway.Application.Mappers
{
    public class MerchantsMapperProfile : Profile
    {
        public MerchantsMapperProfile() : base("Merchants")
        {
            CreateMap<Merchant, AuthenticationUser>();
        }
    }
}