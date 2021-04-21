using Checkout.PaymentGateway.Application.Authentication.Command;
using Checkout.PaymentGateway.Application.Authentication.Service;
using Checkout.PaymentGateway.Application.UnitTests.Common;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Exceptions;
using Checkout.PaymentGateway.Infrastructure.Repositories;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.Application.UnitTests
{
    [Collection("QueryCollection")]
    public class AuthenticateUserCommandTests : CommandTestBase
    {
        private readonly AuthenticateUserCommandHandler _sut;
        private readonly AuthenticateUserCommandValidator _sutValidator;
        private readonly Repository<Merchant> _merchantRepository;
        private readonly MerchantService _merchantService;

        public AuthenticateUserCommandTests(QueryTestFixture fixture)
        {
            _merchantRepository = new Repository<Merchant>(_context);

            _merchantService = new MerchantService(
                _merchantRepository,
                fixture.EncryptionService,
                fixture.AppSettings,
                fixture.Mapper);

            _sut = new AuthenticateUserCommandHandler(_merchantService);
            _sutValidator = new AuthenticateUserCommandValidator();
        }

        [Fact]
        public async Task Handle_GivenValidUser_ShouldGenerateUserToken()
        {
            // Arrange 
            var authCommand = new AuthenticateUserCommand
            {
                MerchantID = "HB123H7123G712",
                ApiKey = "314179fa-7de9-4c9d-8d52-fb6f62ab3815"
            };

            // Act
            var result = await _sut.Handle(authCommand, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_GivenWrongMerchant_ShouldThrownAuthenticationFailException()
        {
            // Arrange
            var authCommand = new AuthenticateUserCommand { MerchantID = "Test", ApiKey = "Test" };

            // Act
            Func<Task> act = async () => { await _sut.Handle(authCommand, CancellationToken.None); };

            // Assert
            await act.Should().ThrowAsync<AuthenticationFailException>();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("TEST", "TESTApiKey")]
        [InlineData("Min5CharData", "")]
        [InlineData(null, null)]
        public async Task Handle_GivenWrongMerchantDataModel_ShouldReturnValidationException(string merchantID, string apiKey)
        {
            // Arrange
            var authCommand = new AuthenticateUserCommand { MerchantID = merchantID, ApiKey = apiKey };

            // Act
            var validationResult = await _sutValidator.ValidateAsync(authCommand);

            // Assert
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
