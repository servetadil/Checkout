using AutoMapper;
using Checkout.PaymentGateway.Application.Payments.Service;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Exceptions;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Queries.GetPaymenDetail
{
    public class GetPaymentDetailQuery : IRequest<GetPaymentDetailVm>
    {
        [Required]
        public Guid PaymentID { get; set; }

        public class GetPaymentDetailQueryHandler : IRequestHandler<GetPaymentDetailQuery, GetPaymentDetailVm>
        {
            private readonly IPaymentService _paymentService;
            private readonly IMapper _mapper;

            public GetPaymentDetailQueryHandler(
                IPaymentService paymentService,
                IMapper mapper)
            {
                _paymentService = paymentService;
                _mapper = mapper;
            }

            public async Task<GetPaymentDetailVm> Handle(GetPaymentDetailQuery request, CancellationToken cancellationToken)
            {
                var payment = await _paymentService.GetPaymentByPaymentID(request.PaymentID);

                if (payment == null)
                    throw new NotFoundException(nameof(Payment), request.PaymentID);

                return _mapper.Map<GetPaymentDetailVm>(payment);
            }
        }
    }
}
