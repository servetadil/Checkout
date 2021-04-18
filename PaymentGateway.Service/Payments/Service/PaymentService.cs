using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Domain.Entities;

namespace Checkout.PaymentGateway.Application.Payments.Service
{
    public class PaymentService : CrudService<Payment>, IPaymentService
    {
        public PaymentService(IRepository<Payment> paymentRepository)
        : base(paymentRepository)
        {
        }
    }
}
