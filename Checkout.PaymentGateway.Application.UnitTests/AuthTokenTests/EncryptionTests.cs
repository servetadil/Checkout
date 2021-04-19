using Checkout.PaymentGateway.Application.Authentication.Service;
using Checkout.PaymentGateway.Application.UnitTests.Common;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Encryption;
using Checkout.PaymentGateway.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Checkout.PaymentGateway.Application.UnitTests.AuthTokenTests
{
    [Collection("QueryCollection")]
    public class EncrptionServiceTests : CommandTestBase
    {
        private readonly EncryptionService _sut;
        private readonly Repository<Merchant> _merchantRepository;
        private readonly MerchantService _merchantService;
        public EncrptionServiceTests(QueryTestFixture fixture)
        {
            _sut = new Mock<EncryptionService>().Object;

            _merchantService = new MerchantService(
                _merchantRepository,
                fixture.EncryptionService,
                fixture.AppSettings,
                fixture.Mapper);
        }

        [Fact]
        public void Encrypt_GivenFakeMerchantDetails_ShouldBeConvertedToJWTToken()
        {
            // Arrange
            var testMerchant = "merchantID";
            var apiKey = "TestApiKey";

            // Act
            var encryptedData = _merchantService.GenerateJwtToken(testMerchant, apiKey);

            // Assert
            encryptedData.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Encrypt_GivenText_ShouldBeEncrypted()
        {
            // Arrange
            var testString = "Test String";

            // Act
            var encryptedData = _sut.Encrypt(testString);

            // Assert
            encryptedData.Should().NotBeEquivalentTo(testString);
        }

        [Fact]
        public void Decrypt_EncrptedString_ShouldBeDecrypted()
        {
            // Arrange
            var testString = "Test String";

            // Act
            var encrptedData = _sut.Encrypt(testString);
            var decryptedData = _sut.Decrypt(encrptedData);

            // Assert
            decryptedData.Should().NotBeEquivalentTo(encrptedData);
            decryptedData.Should().BeEquivalentTo(testString);
        }

    }
}
