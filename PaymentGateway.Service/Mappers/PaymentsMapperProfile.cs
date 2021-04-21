using AutoMapper;
using Checkout.PaymentGateway.Application.Payments.Dto;
using Checkout.PaymentGateway.Application.Payments.Queries.GetPaymenDetail;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Common;
using Checkout.PaymentGateway.Helper.Encryption;

namespace Checkout.PaymentGateway.Application.Mappers
{
    public class PaymentsMapperProfile : Profile
    {
        public PaymentsMapperProfile()
            : base("Payments")
        {
            CreateMap<Payment, PaymentDTO>()
                .ForMember(x => x.PaymentStatusCode, opts => opts.MapFrom(src => src.PaymentStatus));

            CreateMap<Payment, GetPaymentDetailVm>()
                .ForMember(x => x.PaymentStatusCode, opts => opts.MapFrom(src => src.PaymentStatus))
                .ForMember(x => x.CardNumber, opts => opts.MapFrom(src =>
                    CardHelper.MaskCardNumber(src.Card.CardNumber)));


        }
    }
}