using Checkout.PaymentGateway.Application.Authentication.Service;
using Checkout.PaymentGateway.Application.UnitTests.Common;
using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Domain.Entities;
using Checkout.PaymentGateway.Helper.Encryption;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Checkout.PaymentGateway.Application.UnitTests.AuthTokenTests
{
    public class EncrptionServiceTests : CommandTestBase
    {
        private readonly EncryptionService _sut;
        private readonly MerchantService _sut2;
        public EncrptionServiceTests()
        {
            _sut = new Mock<EncryptionService>().Object;
            _sut2 = new Mock<MerchantService>().Object;
        }
        [Fact]
        public void Encrypt_GivenText_ShouldBeEncryptedGener()
        {
            // Arrange
            var testString = "Test String";

            // Act
            var encryptedData = _sut2.GenerateJwtToken("test", "test");

            // Assert
            encryptedData.Should().NotBeEquivalentTo(testString);
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
