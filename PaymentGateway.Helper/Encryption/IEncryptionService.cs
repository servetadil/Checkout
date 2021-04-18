using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Helper.Encryption
{
    public interface IEncryptionService
    {
        public string Encrypt(string text);

        public string Decrypt(string text);
    }
}
