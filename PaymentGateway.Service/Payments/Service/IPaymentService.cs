using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Service
{
    public interface IPaymentService : ICrudService<Payment>
    {
        Task<Payment> GetPaymentByPaymentID(Guid paymentID);

        Task<IEnumerable<Payment>> GetPaymentListCached();
    }
}
