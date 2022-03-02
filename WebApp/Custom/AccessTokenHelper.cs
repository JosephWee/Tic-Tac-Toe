using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WebApp
{
    public class AccessTokenHelper
    {
        private static X509Certificate2 _cert { get; set; }
        private static string _fileNotFoundExceptionMessage = "Certificate file not found. Call the LoadCertificate method first.";

        public static void LoadCertificate(string certFilePath, string password)
        {
            if (!File.Exists(certFilePath))
                throw new FileNotFoundException(_fileNotFoundExceptionMessage);

            byte[] bytes = File.ReadAllBytes(certFilePath);
            _cert = new X509Certificate2(bytes, password);
        }

        public static string HashAndSign(string data)
        {
            if (_cert == null)
                throw new FileNotFoundException(_fileNotFoundExceptionMessage);

            byte[] dataBytes = System.Text.Encoding.Unicode.GetBytes(data);
            byte[] signedHashBytes = HashAndSign(dataBytes);

            return Convert.ToBase64String(signedHashBytes);
        }

        public static byte[] HashAndSign(byte[] data)
        {
            if (_cert == null)
                throw new FileNotFoundException(_fileNotFoundExceptionMessage);

            //RSAParameters publicKeyParams = _cert.GetRSAPublicKey().ExportParameters(false);
            RSA rsa = _cert.GetRSAPrivateKey();
            //RSAParameters parameters = rsa.ExportParameters(true);
            
            SHA512Managed hash = new SHA512Managed();
            byte[] dataHash = hash.ComputeHash(data);

            byte[] signedHashBytes = rsa.SignHash(dataHash, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);

            return signedHashBytes;
        }
    }
}