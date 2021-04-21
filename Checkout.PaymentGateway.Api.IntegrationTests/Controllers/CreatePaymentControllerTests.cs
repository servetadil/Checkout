using Checkout.PaymentGateway.Api.IntegrationTests.Common;
using Checkout.PaymentGateway.Application.Authentication.User;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using System;

namespace Checkout.PaymentGateway.Api.IntegrationTests
{
    public class CreatePaymentControllerTests : IClassFixture<CheckoutWebApiApplicationFactory<Startup>>
    {
        private const string TestApiKey = "314179fa-7de9-4c9d-8d52-fb6f62ab3815";
        private const string TestMerchantID = "HB123H7123G712";

        private readonly CheckoutWebApiApplicationFactory<Startup> _factory; 

        public CreatePaymentControllerTests(CheckoutWebApiApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreatePayment_GivenCorrectUser_ShouldReturnAuthToken()
        {
            // Arrange 
            var client = await _factory.GetAuthenticatedClientAsync(TestMerchantID, TestApiKey);

            var command = new CreatePaymentCommand()
            {
                Amount = 10,
                Currency = "EUR",
                OrderID = "TESTORDERID"
            };

            // Act
            var response = await client.PostAsync($"/api/create-payment", Utils.GetRequestContent(command));

            var payment = await Utils.GetResponseContent<CreatePaymentResultWm>(response);

            // Assert
            payment.OrderID.Should().NotBeEmpty();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(10, "EUR", "")]
        [InlineData(10, "EURRRR", "314179fa-7de9-4c9d-8d52-fb6f62ab3815")]
        [InlineData(0, "EURRRR", "314179fa-7de9-4c9d-8d52-fb6f62ab3815")]
        public async Task CreatePayment_GivenWrongAmount_ShouldThrownBadRequestException
            (decimal amount, string currency, string orderId)
        {
            // Arrange 
            var client = await _factory.GetAuthenticatedClientAsync(TestMerchantID, TestApiKey);

            var command = new CreatePaymentCommand()
            {
                Amount = amount,
                Currency = currency,
                OrderID = orderId
            };

            // Act
            var response = await client.PostAsync($"/api/create-payment", Utils.GetRequestContent(command));

            var payment = await Utils.GetResponseContent<CreatePaymentResultWm>(response);
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            payment.OrderID.Should().BeEmpty();
        }
    }
}

