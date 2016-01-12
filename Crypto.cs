using System;
using RevStack.Cryptography;

namespace RevStack.Mvc
{
    
    public class Crypto
    {
        private readonly string _key;
        
        public Crypto(string key)
        {
            this._key = key;
        }

        /// <summary>
        /// Encrypt plain text via Rijndael symmetric-key algorithm. Returns Base64 string representation of the encrypted text
        /// </summary>
        /// <param name="plainText">the plain text</param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            string eKey = this._key;
            var sym = new Symmetric(Symmetric.Provider.Rijndael);
            var key = new Data(eKey);
            var encryptedText = sym.Encrypt(new Data(plainText), key);
            return encryptedText.ToBase64();
        }

        /// <summary>
        ///  Decrypt encrypted text via Rijndael symmetric-key algorithm. Returns the plain text
        /// </summary>
        /// <param name="encryptedText">Base64 string representation of the encrypted text</param>
        /// <returns></returns>
        public string Decrypt(string encryptedText)
        {
            try
            {
                string eKey = this._key;
                var sym = new Symmetric(Symmetric.Provider.Rijndael);
                var key = new Data(eKey);
                var encryptedData = new Data {Base64 = encryptedText};
                return sym.Decrypt(encryptedData, key).ToString();
            }
            catch(Exception)
            {
                return null;
            }
        }


    }
}