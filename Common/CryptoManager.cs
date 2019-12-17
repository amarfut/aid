using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class CryptoManager
    {
        private string Key = "youIT-81e223fj23f2930j32f";
        private string Vector = "qwerty";

        public string Encrypt(string text)
        {
            if (text == null) return null;
            string encryptedText = string.Empty;
            using (SymmetricAlgorithm aes = new AesCryptoServiceProvider())
            {
                aes.Key = Get256BitHash(Key);
                aes.IV = Get128BitHash(Vector);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                        encryptedText = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            return encryptedText;
        }

        public string Decrypt(string encryptedString)
        {
            if (encryptedString == null) return null;

            string plainText = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Get256BitHash(Key);
                aes.IV = Get128BitHash(Vector);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedString)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }



        private byte[] Get256BitHash(string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        private byte[] Get128BitHash(string value)
        {
            using (MD5 sha = MD5.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }
    }
}
