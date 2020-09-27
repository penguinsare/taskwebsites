using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaskWebsites.Services
{
    public class PasswordSafe : IPasswordSafe
    {
        private readonly byte[] _key;
        private readonly SymmetricAlgorithm _algorithm;
        public PasswordSafe(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            _key = Encoding.UTF8.GetBytes(key);
            _algorithm = SymmetricAlgorithm.Create("Aes");
            _algorithm.Padding = PaddingMode.PKCS7;
        }
        public string Decrypt(string encryptedPassword)
        {
            byte[] initVectorAndEncryptedBytePassword = Convert.FromBase64String(encryptedPassword);
            byte[] initVector = new byte[16];
            byte[] encryptedBytePassword = new byte[initVectorAndEncryptedBytePassword.Length - 16];
            Buffer.BlockCopy(initVectorAndEncryptedBytePassword, 0, initVector, 0, 16);
            Buffer.BlockCopy(initVectorAndEncryptedBytePassword, 16, encryptedBytePassword, 0, initVectorAndEncryptedBytePassword.Length - 16);
            ICryptoTransform dencryptor = _algorithm.CreateDecryptor(_key, initVector);
            byte[] decryptedBytePassword = dencryptor.TransformFinalBlock(encryptedBytePassword, 0, encryptedBytePassword.Length);
            return Encoding.UTF8.GetString(decryptedBytePassword);
        }

        public string Encrypt(string password)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(password);            
            ICryptoTransform encryptor = _algorithm.CreateEncryptor(_key, _algorithm.IV);
            byte[] encryptedBytePassword = encryptor.TransformFinalBlock(bytePassword, 0, bytePassword.Length);
            byte[] initVectorAndEncryptedBytePassword = new byte[16 + encryptedBytePassword.Length];
            Buffer.BlockCopy(_algorithm.IV, 0, initVectorAndEncryptedBytePassword, 0, 16);
            Buffer.BlockCopy(encryptedBytePassword, 0, initVectorAndEncryptedBytePassword, 16, encryptedBytePassword.Length);
            return Convert.ToBase64String(initVectorAndEncryptedBytePassword);
        }
    }
}
