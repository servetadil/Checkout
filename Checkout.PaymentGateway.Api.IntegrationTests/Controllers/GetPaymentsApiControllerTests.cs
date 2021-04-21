using Checkout.PaymentGateway.Api.IntegrationTests.Common;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using Checkout.PaymentGateway.Application.Payments.Queries.GetPaymenDetail;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System;
using System.Net.Http;

namespace Checkout.PaymentGateway.Api.IntegrationTests
{
    public class GetPaymentsApiControllerTests : IClassFixture<CheckoutWebApiApplicationFactory<Startup>>
    {
        private const string TestApiKey = "314179fa-7de9-4c9d-8d52-fb6f62ab3815";
        private const string TestMerchantID = "HB123H7123G712";

        private readonly CheckoutWebApiApplicationFactory<Startup> _factory;

        public GetPaymentsApiControllerTests(CheckoutWebApiApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetPaymentQuery_GivenCorrectPaymentID_ShouldReturnCorrectPayment()
        {
            // Arrange 
            var client = await _factory.GetAuthenticatedClientAsync(TestMerchantID, TestApiKey);
            var generatedPaymentOrderID = await GeneratePayment(client);

            // Act
            var submittedResponse = await client.GetAsync($"/api/get-payment/{generatedPaymentOrderID}");
            var payment = await Utils.GetResponseContent<GetPaymentDetailVm>(submittedResponse);

            // Assert
            payment.Should().NotBeNull();
            payment.OrderID.Should().NotBeNull();
        }

        public async Task<Guid> GeneratePayment(HttpClient client)
        {
            var command = new CreatePaymentCommand()
            {
                Amount = 10,
                Currency = "EUR",
                OrderID = Guid.NewGuid().ToString()
            };

            var createPaymentResponse = await client.PostAsync($"/api/create-payment", Utils.GetRequestContent(command));
            var createdPayment = await Utils.GetResponseContent<CreatePaymentResultWm>(createPaymentResponse);

            return createdPayment.OrderID;
        }
    }
}

