using Checkout.PaymentGateway.Application.Payments.Dto;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Application.Payments.Queries.GetPaymentList
{
    public class GetPaymentListVm
    {
        public IEnumerable<PaymentDTO> PaymentList { get; set; }
    }
}
