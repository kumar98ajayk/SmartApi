using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SmartApi.Commons.EncodeDecode
{
    public class EncodeDecode : IEncodeDecode
    {
        public string _SCOMMEncryptString(string key, string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            string encryptedText;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key.PadRight(32, ' ')); // Ensure the key is 32 bytes long
                aesAlg.IV = new byte[16]; // Initialization vector with zeros. Consider using a random IV for better security.

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encryptedText = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }

            return encryptedText;
        }
    }

    public interface IEncodeDecode
    {
        string _SCOMMEncryptString(string key, string plainText);
    }
}
