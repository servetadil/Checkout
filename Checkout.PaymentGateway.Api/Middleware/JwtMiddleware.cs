using Checkout.PaymentGateway.Application.Authentication.Service;
using Checkout.PaymentGateway.Helper.Common;
using Checkout.PaymentGateway.Helper.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationSettings _appSettings;
        private readonly IEncryptionService _encryptionService;

        public JwtMiddleware(
            RequestDelegate next,
            ApplicationSettings appSettings,
            IEncryptionService encryptionService)
        {
            _next = next;
            _appSettings = appSettings;
            _encryptionService = encryptionService;
        }

        public async Task Invoke(HttpContext context, IMerchantService merchantService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await InitializeUserToContext(context, merchantService, _encryptionService.Decrypt(token));

            await _next(context);
        }

        private async Task InitializeUserToContext(HttpContext context, IMerchantService merchantService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var merchantId = jwtToken.Claims.First(x => x.Type == "merchantId").Value.ToString();
                var apiKey = jwtToken.Claims.First(x => x.Type == "apiKey").Value.ToString();

                // attach user to context on successful jwt validation
                context.Items["User"] = await merchantService.GetMerchant(merchantId, apiKey);
            }
            catch
            {
                throw new SecurityTokenExpiredException();
            }
        }
    }
}
