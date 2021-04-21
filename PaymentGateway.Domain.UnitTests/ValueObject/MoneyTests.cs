using Checkout.PaymentGateway.Domain.SharedKernel;
using System;
using Xunit;
using FluentAssertions;

namespace PaymentGateway.Domain.UnitTests
{
    public class MoneyTests
    {
        [Fact]
        public void SetCurrency_GivenNullCurrencyCode_ShouldThrownArgumentException()
        {
            Action action = () => new Money(10, null);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetCurrency_GivenZeroAmount_ShouldThrown()
        {
            Action action = () => new Money(0, "EUR");
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
