using Checkout.PaymentGateway.Application.Authentication.Command;
using Checkout.PaymentGateway.Application.Authentication.User;
using Checkout.PaymentGateway.Infrastructure.DatabaseFactory;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Checkout.PaymentGateway.Api.IntegrationTests.Common
{
    public class CheckoutWebApiApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            return await GetAuthenticatedClientAsync("HB123H7123G712", "314179fa-7de9-4c9d-8d52-fb6f62ab3815");
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync(string merchantID, string apiKey)
        {
            var client = CreateClient();

            var token = await GetAccessTokenAsync(client, merchantID, apiKey);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        private async Task<string> GetAccessTokenAsync(HttpClient client, string merchantID, string apiKey)
        {
            var authUser = new AuthenticateUserCommand
            {
                MerchantID = merchantID,
                ApiKey = apiKey
            };

            var response = await client.PostAsync($"/api/authenticate", Utils.GetRequestContent(authUser));

            var responseUser = await Utils.GetResponseContent<AuthenticationUser>(response);

            response.EnsureSuccessStatusCode();

            return responseUser.Secret;
        }
    }
}
