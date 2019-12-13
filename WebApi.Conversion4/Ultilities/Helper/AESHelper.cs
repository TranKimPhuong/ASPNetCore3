using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Conversion4.Ultilities.Helper
{
    public class AESHelper
    {

        public static string DecryptAES(string encryptStr, string key)
        {
            var encryptedBytes = Convert.FromBase64String(encryptStr);
            var result = Encoding.UTF8.GetString(DecryptAES(encryptedBytes, key));
            return result;
        }

        public static string EncryptAES(string content, string key)
        {
            var plainBytes = new StringBuilder(content);
            var result = Convert.ToBase64String(EncryptAES(plainBytes, key));
            return result;
        }
        // follow to https://www.c-sharpcorner.com/article/aes-encryption-in-c-sharp/
        // and https://jonlabelle.com/snippets/view/csharp/encrypt-and-decrypt-data-in-dotnet-core
        public static byte[] EncryptAES(StringBuilder inputContent, string secretKey)
        {
            byte[] encrypted;
    
            using (AesManaged aes = new AesManaged())
            {
                var keyBytes = new byte[16];
                var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
  
                ICryptoTransform encryptor = aes.CreateEncryptor(keyBytes, keyBytes);
   
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    { 
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(inputContent);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static byte[] DecryptAES(byte[] inputContent, string secretKey)
        {
            using (AesManaged aes = new AesManaged())
            {
                var keyBytes = new byte[16];
                var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));

                ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, keyBytes);
  
                using (MemoryStream ms = new MemoryStream(inputContent))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        return ms.ToArray();
                    }
                }
            }

        }
    }
}
