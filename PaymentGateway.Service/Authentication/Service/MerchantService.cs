using AutoMapper;
using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Application.Common.Services;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Common;
using Checkout.PaymentGateway.Helper.Encryption;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Application.Authentication.Service
{
    public class MerchantService : CrudService<Merchant>, IMerchantService
    {
        private readonly IMapper _mapper;

        private readonly ApplicationSettings _appSettings;

        private readonly IEncryptionService _encryptionService;

        public MerchantService(
            IRepository<Merchant> merchantRepository,
            IEncryptionService encryptionService,
            ApplicationSettings appSettings,
            IMapper mapper)
        : base(merchantRepository)
        {
            _appSettings = appSettings;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        public async Task<AuthenticationUser> GetMerchant(string merchantId, string apiKey)
        {
            var user = await _repository.SingleAsync(x => x.MerchantID == merchantId && x.ApiKey == apiKey);

            return _mapper.Map<AuthenticationUser>(user);
        }

        public async Task<AuthenticationUser> AuthenticateAndGenerateApiSecret(string merchantId, string apiKey)
        {
            var user = await GetMerchant(merchantId, apiKey);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(merchantId, apiKey);
            var authenticationUser = _mapper.Map<AuthenticationUser>(user);

            authenticationUser.Secret = token;

            return authenticationUser;
        }

        public string GenerateJwtToken(string merchantId, string apiKey)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("merchantId", merchantId),
                    new Claim("apiKey", apiKey)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return _encryptionService.Encrypt(tokenHandler.WriteToken(token));
        }

    }
}
