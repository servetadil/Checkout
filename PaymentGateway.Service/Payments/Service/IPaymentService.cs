using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Entities;

namespace Checkout.PaymentGateway.Application.Payments.Service
{
    public interface IPaymentService : ICrudService<Payment>
    {

    }
}
