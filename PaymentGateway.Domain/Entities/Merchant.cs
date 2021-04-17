using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Checkout.PaymentGateway.Domain.Entities
{
    public class Merchant : Entity
    {
        [Required]
        public string MerchantID { get; set; }

        [Required]
        public string ApiKey { get; set; }

        public Merchant()
        {

        }

        public Merchant(string merchantId, string apiKey)
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
