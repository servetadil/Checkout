using PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Entities
{
    public class Merchant : Entity
    {
        public string MerchantID { get; set; }

        public string ApiKey { get; set; }

        private Merchant(string merchantId, string apiKey)
        {
            SetMerchantID(merchantId);
            SetApiKey(apiKey);
        }

        public void SetMerchantID(string merchantId)
             => MerchantID = merchantId ?? throw new ArgumentNullException(nameof(merchantId));

        public void SetApiKey(string apiKey)
             => ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }
}
