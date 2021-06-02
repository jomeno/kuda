using System;
using System.Security.Cryptography;
using System.Text;

namespace Kuda.Domain.Security
{
    public static class Hasher
    {
        // public static void EncryptRSA2(string phrase)
        // {
        //     var publicKey = "";
        //     var phraseBytes = Encoding.UTF8.GetBytes(phrase);


        // }

        public static void EncryptRSA(string phrase = "Data to Encrypt")
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes(phrase);
                byte[] encryptedData;
                byte[] decryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Pass the data to ENCRYPT, the public key information 
                    //(using RSACryptoServiceProvider.ExportParameters(false),
                    //and a boolean flag specifying no OAEP padding.
                    var rsaPublicKey = new RSAParameters
                    {
                        Modulus = Encoding.UTF8.GetBytes("6D7ReroDASrRtCl6ZHbSbXPOGcUmITekiOcugj6gm6+uod/SLLP9hkbAdBZcScNME80JSZqnyq4f5TggJJfVq/5Psn0e29U2ZDGd9EdGrvUnpoumCzisgkLB6Q/m5MBQxyfsL7WfTx0RcTrI2NuvErI+KfKv9rSZvoK1Xv3IhT0="),
                        Exponent = Encoding.UTF8.GetBytes("AQAB")
                    };

                    encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                    //Pass the data to DECRYPT, the private key information 
                    //(using RSACryptoServiceProvider.ExportParameters(true),
                    //and a boolean flag specifying no OAEP padding.
                    var rsaPrivateKey = new RSAParameters
                    {
                        Modulus = Encoding.UTF8.GetBytes("qPxvXQtdHV/uAeyjLElRAayzmhhaadT0lCCL4RxkJXI1PIWb+bVlpmDVunJTYgUhuJANcDi61AF/Q90MDncuP46So+C0qtMhSakZIFIoWMgYLmgcRoj9ziAfXdsHpBza/vU3RjLkTm8d2dYyPTZdMm8U723RqoRs9l+2EkK6Zu0="),
                        Exponent = Encoding.UTF8.GetBytes("AQAB"),
                        P = Encoding.UTF8.GetBytes("1IGYphY2ugfA10zHNp1pDanhrXGcLrZNrTI8Uo0hynMsrG8xF+pe23tOfr7J+oYvmN60jPTYJ6lagQU6sic96w=="),
                        Q = Encoding.UTF8.GetBytes("y5KSQYD6S82zWSNATSkvnHRqA6woB6g+JqndbCd9BGAim+rtuWqwPREyezpcEarnDHHAUA8XTY3lI0tfCCpAhw=="),
                        DP = Encoding.UTF8.GetBytes("lReHnZ8gLkyaQ3OeoPa3adqydxmqViuZO9Zu9AwIlR1RTnmEnB7XBm3wmIQK+TWD12EIk4yEyu7KjJK6p5tYmQ=="),
                        DQ = Encoding.UTF8.GetBytes("W1qhK6gXqqDa1SuOlZHf/dP4J0HAjP8hNPSRmxF1dts1bMbWe5i3EhB/mPEtk/gfy2PYq5S6HmGI7HfMY7uiTQ=="),
                        InverseQ = Encoding.UTF8.GetBytes("SvxiSbNLy6/WGZDgBNHAA3tltvbWEj086hQsHGyPauwMzjACaI9xrmX++XPtJIi5D6vFcmfs7Dzo3LjYIADb1g=="),
                        D = Encoding.UTF8.GetBytes("MBv+EH1FuzEub3nRUrBk0Zc7YqmARBUOtIU3jZUppceIBHz9VPAhymZTMsuNlaBkY0kPql1cQzNR6h4qaovfrF4Tv5kcnSqnZlT9v0QybCGcnMEAYFXpZVSU6fUuZZo7cfZC2A1cXOnmY2f/EOs+t4KP310OVS0VW7qqPtCHHXk=")
                    };
                    decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                    //Display the decrypted plaintext to the console. 
                    Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
                }
            }
            catch (ArgumentNullException ex)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine($"Encryption failed. {ex.Message}");
            }

        }

        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }


    }

}