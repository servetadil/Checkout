using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Service
{
    public class PaymentService : CrudService<Payment>, IPaymentService
    {
        public PaymentService(IRepository<Payment> paymentRepository)
        : base(paymentRepository)
        {
        }

        public async Task<Payment> GetPaymentByPaymentID(Guid paymentId, string merchantId, string apiKey)
        {
            var entity = await _repository.SingleAsync(
                x => x.PaymentID == paymentId &&
                x.MerchantID == merchantId &&
                x.ApiKey == apiKey);

            return entity;
        }
    }
}
