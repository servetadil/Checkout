using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Application.Common;
using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Payments.Service
{
    public class PaymentService : CrudService<Payment>, IPaymentService
    {
        private readonly IAuthorizationService _authService;
        private readonly AuthenticationUser _contextUser;
        private readonly IEncryptionService _encryptionService;

        public PaymentService(IRepository<Payment> paymentRepository,
             IAuthorizationService authService,
             IEncryptionService encryptionService)
        : base(paymentRepository)
        {
            _authService = authService;
            _contextUser = _authService.GetAuthenticatedMerchant();
            _encryptionService = encryptionService;
        }

        public async Task<Payment> GetPaymentByPaymentID(Guid paymentId)
        {
            var entity = await _repository.SingleAsync(
                x => x.PaymentID == paymentId &&
                x.MerchantID == _contextUser.MerchantID &&
                x.ApiKey == _contextUser.ApiKey);

            if (entity.Card != null)
                entity.Card.CardNumber = _encryptionService.Decrypt(entity.Card.CardNumber);

            return entity;
        }

        public async Task<IEnumerable<Payment>> GetPaymentListCached()
        {
            var entity = await _repository.GetAllAsync(x =>
                x.MerchantID == _contextUser.MerchantID &&
                x.ApiKey == _contextUser.ApiKey);

            return entity;
        }
    }
}
