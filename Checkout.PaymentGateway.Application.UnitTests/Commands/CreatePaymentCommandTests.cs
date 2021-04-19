using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using Checkout.PaymentGateway.Application.Payments.Service;
using Checkout.PaymentGateway.Application.UnitTests.Common;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Infrastructure.Repositories;
using FluentAssertions;
using System.Threading;
using Xunit;

namespace Checkout.PaymentGateway.Application.UnitTests
{
    public class CreatePaymentCommandTests : CommandTestBase
    {
        private readonly CreatePaymentCommandHandler _sut;
        private readonly CreatePaymentCommandValidator _sutValidator;
        private readonly Repository<Payment> _paymentRepository;
        private readonly PaymentService _paymentService;

        public CreatePaymentCommandTests()
        {
            _paymentRepository = new Repository<Payment>(_context);
            _paymentService = new PaymentService(_paymentRepository);
            _sut = new CreatePaymentCommandHandler(_paymentService);
            _sutValidator = new CreatePaymentCommandValidator();

        }

        [Fact]
        public void Handle_GivenValidPaymentRequest_ShouldReturnPaymentId()
        {            
            // Arrange
            var paymentCommand = new CreatePaymentCommand() { Amount = 12, Currency = "EUR", OrderID = "TEST" };

            // Act
            var result = _sut.Handle(paymentCommand, CancellationToken.None);

            // Assert
            result.IsCompleted.Should().BeTrue();
            result.Exception.Should().BeNull();
            result.Result.Should().NotBeEmpty();
        }

        [Fact]
        public void Handle_GivenValidPaymentRequest_ShouldNotHaveValidationError()
        {
            // Arrange
            var paymentCommand = new CreatePaymentCommand() { Amount = 12, Currency = "EUR", OrderID = "TEST5" };

            // Act
            var validationResult = _sutValidator.Validate(paymentCommand);

            // Assert
            validationResult.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData(12, "EUR", "")]
        [InlineData(12, "EUR", null)]
        [InlineData(10, "EUR", "TEST")]
        [InlineData(0, "EUR", "TESTORDERID")]
        [InlineData(10, "", "TESTORDERID")]
        [InlineData(10, null, "TESTORDERID")]
        [InlineData(10, "EU", "TESTORDERID")]
        public void Handle_GivenWrongData_ShouldReturnValidationException(
            decimal amount,
            string currency,
            string orderId)
        {
            // Arrange
            var paymentCommand = new CreatePaymentCommand() { Amount = amount, Currency = currency, OrderID = orderId };

            // Act
            var validationResult = _sutValidator.Validate(paymentCommand);

            // Assert
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
