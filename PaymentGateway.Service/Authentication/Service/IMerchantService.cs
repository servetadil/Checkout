using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Authentication.Service
{
    public interface IMerchantService : ICrudService<Merchant>
    {
        Task<AuthenticationUser> GetMerchant(string merchantId, string apiKey);

        Task<AuthenticationUser> AuthenticateAndGenerateApiSecret(string merchantId, string apiKey);
    }
}
