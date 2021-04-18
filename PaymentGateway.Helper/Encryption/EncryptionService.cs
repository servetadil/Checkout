using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Checkout.PaymentGateway.Helper.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private TripleDESCryptoServiceProvider m_des = new TripleDESCryptoServiceProvider();

        private string keyValue = "_SviddNeger_Application_";
        private string keyIV = "_ulver43135sviddne__ger_";

        private byte[] m_key;
        private byte[] m_iv;
        private UTF8Encoding m_utf8 = new UTF8Encoding();

        public EncryptionService()
        {
            m_key = m_utf8.GetBytes(keyValue);
            m_iv = m_utf8.GetBytes(keyIV);
        }

        public string Encrypt(string text)
        {
            byte[] input = m_utf8.GetBytes(text);
            byte[] output = Transform(input, m_des.CreateEncryptor(m_key, m_iv));
            text = Convert.ToBase64String(output);
            return text;
        }

        public string Decrypt(string text)
        {
            byte[] input = Convert.FromBase64String(text);
            byte[] output = Transform(input, m_des.CreateDecryptor(m_key, m_iv));
            return m_utf8.GetString(output);
        }

        private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();
            memStream.Position = 0;

            byte[] result = memStream.ToArray();

            memStream.Close();
            cryptStream.Close();

            return result;
        }
    }
}
