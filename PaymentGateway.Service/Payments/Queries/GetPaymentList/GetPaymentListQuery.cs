using AutoMapper;
using Checkout.PaymentGateway.Application.Payments.Dto;
using Checkout.PaymentGateway.Application.Payments.Service;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Queries.GetPaymentList
{
    public class GetPaymentListQuery : IRequest<GetPaymentListVm>
    {
        public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQuery, GetPaymentListVm>
        {
            private readonly IPaymentService _paymentService;
            private readonly IMapper _mapper;

            public GetPaymentListQueryHandler(
                IPaymentService paymentService,
                IMapper mapper)
            {
                _paymentService = paymentService;
                _mapper = mapper;
            }

            public async Task<GetPaymentListVm> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
            {
                var payments = await _paymentService.GetPaymentListCached();
                
                return new GetPaymentListVm() { PaymentList = _mapper.Map<List<PaymentDTO>>(payments) };
            }
        }
    }
}

