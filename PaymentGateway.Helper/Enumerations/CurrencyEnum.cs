using Checkout.PaymentGateway.Helper.Common;
using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Helper.Enums
{
    public class CurrencyEnum : Enumeration
    {
        public static CurrencyEnum UsdDollar = new CurrencyEnum("USD");
        public static CurrencyEnum Euro = new CurrencyEnum("EUR");
        public static CurrencyEnum SwissFranc = new CurrencyEnum("CHF");

        protected CurrencyEnum() { }

        public CurrencyEnum(string name)
            : base(name)
        {
        }

        public static CurrencyEnum GetFromCode(string code)
        {
            switch (code)
            {
                case "USD":
                    return UsdDollar;
                case "EUR":
                    return Euro;
                case "CHF":
                    return SwissFranc;
                default:
                    throw new Exception($"Invalid code {code}");
            }
        }

        public static IEnumerable<CurrencyEnum> SupportedCurrencies()
        {
            return new[] { UsdDollar, Euro, SwissFranc };
        }
    }
}
