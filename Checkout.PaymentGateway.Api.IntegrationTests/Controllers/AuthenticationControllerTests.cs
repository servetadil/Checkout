using Checkout.PaymentGateway.Api.IntegrationTests.Common;
using Checkout.PaymentGateway.Application.Authentication.Command;
using Checkout.PaymentGateway.Application.Authentication.User;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Checkout.PaymentGateway.Api.IntegrationTests
{
    public class AuthenticationControllerTests : IClassFixture<CheckoutWebApiApplicationFactory<Startup>>
    {
        private const string TestApiKey = "314179fa-7de9-4c9d-8d52-fb6f62ab3815";
        private const string TestMerchantID = "HB123H7123G712";

        private readonly CheckoutWebApiApplicationFactory<Startup> _factory;

        public AuthenticationControllerTests(CheckoutWebApiApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Authenticate_GivenCorrectUser_ShouldReturnAuthToken()
        {
            // Arrange 
            var client = _factory.GetAnonymousClient();

            var command = new AuthenticateUserCommand()
            {
                MerchantID = TestMerchantID,
                ApiKey = TestApiKey
            };

            var content = Utils.GetRequestContent(command);

            // Act
            var response = await client.PostAsync($"/api/authenticate", content);
            var authUser = await Utils.GetResponseContent<AuthenticationUser>(response);

            // Assert
            authUser.Secret.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Test_With_Wrong_36_character_apiKey_", TestMerchantID)]
        [InlineData(TestApiKey, "WrongMerchantID")]
        public async Task Authenticate_GivenWrongUserData_ShouldThrownException(string apiKey, string merchantID)
        {
            // Arrange 
            var client = _factory.GetAnonymousClient();

            var command = new AuthenticateUserCommand()
            {
                MerchantID = merchantID,
                ApiKey = apiKey
            };

            var content = Utils.GetRequestContent(command);

            // Act
            var response = await client.PostAsync($"/api/authenticate", content);

            var authUser = await Utils.GetResponseContent<AuthenticationUser>(response);

            // Assert
            authUser.Secret.Should().BeNullOrEmpty();
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
