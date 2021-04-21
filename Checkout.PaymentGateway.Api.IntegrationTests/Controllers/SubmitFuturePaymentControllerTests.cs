using Checkout.PaymentGateway.Api.IntegrationTests.Common;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using Checkout.PaymentGateway.Application.Payments.Commands.SubmitFuturePayment;
using Checkout.PaymentGateway.Helper.Enums;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.Api.IntegrationTests.Controllers
{
    public class SubmitFuturePaymentControllerTests : IClassFixture<CheckoutWebApiApplicationFactory<Startup>>
    {
        private const string TestApiKey = "314179fa-7de9-4c9d-8d52-fb6f62ab3815";
        private const string TestMerchantID = "HB123H7123G712";

        private readonly CheckoutWebApiApplicationFactory<Startup> _factory;

        public SubmitFuturePaymentControllerTests(CheckoutWebApiApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SubmitFuturePayment_GivenCorrectPaymentInfo_ShouldReturnSuccess()
        {
            // Arrange 
            var client = await _factory.GetAuthenticatedClientAsync(TestMerchantID, TestApiKey);
            var generatedPaymentOrderID = await GeneratePayment(client);

            var submitCommand = new SubmitFuturePaymentCommand()
            {
                CardName = "Test CardName",
                CvvCode = "545",
                CardNumber = "5555555555554444",
                ExpiryMonth = 12,
                ExpiryYear = 2024,
                PaymentID = generatedPaymentOrderID
            };

            // Act
            var submittedResponse = await client.PostAsync($"/api/submit-future-payment", Utils.GetRequestContent(submitCommand));
            var payment = await Utils.GetResponseContent<SubmitFuturePaymentResultWm>(submittedResponse);

            // Assert
            payment.OrderID.Should().NotBeNullOrEmpty();
            payment.ResponseCode.Should().Be(PaymentProcessEnum.RequestFuturePayment.Id);
            submittedResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("1111-2222-3333-4444")]
        [InlineData("111122223333444466666666666")]
        [InlineData("StringCardNumber")]
        public async Task SubmitPayment_GivenInvalidCardNumber_ShouldThrownValidationException(string cardNumber)
        {
            // Arrange 
            var client = await _factory.GetAuthenticatedClientAsync(TestMerchantID, TestApiKey);
            var generatedPaymentOrderID = await GeneratePayment(client);

            var submitCommand = new SubmitFuturePaymentCommand()
            {
                CardName = "Test CardName",
                CvvCode = "545",
                CardNumber = cardNumber,
                ExpiryMonth = 12,
                ExpiryYear = 2024,
                PaymentID = generatedPaymentOrderID
            };

            // Act
            var submittedResponse = await client.PostAsync($"/api/submit-future-payment", Utils.GetRequestContent(submitCommand));

            // Assert
            submittedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
