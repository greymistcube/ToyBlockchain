﻿using System;
using System.Text;
using System.Security.Cryptography;

namespace ToyBlockChain.Crypto
{
    public static class CryptoUtil
    {
        public static readonly int NONCE_LENGTH = 8;
        private static RandomNumberGenerator _rng;
        private static SHA256 _sha256;
        private static RSACryptoServiceProvider _rsa;

        static CryptoUtil()
        {
            _rng = RandomNumberGenerator.Create();
            _sha256 = SHA256.Create();
            _rsa = new RSACryptoServiceProvider();
        }

        public static byte[] HashBytes(byte[] bytes)
        {
            return _sha256.ComputeHash(bytes);
        }

        public static string HashString(string str)
        {
            return Convert.ToBase64String(
                _sha256.ComputeHash(Encoding.UTF8.GetBytes(str)));
        }

        public static string Sign(string data, RSAParameters rsaParameters)
        {
            // TODO: Possible Security hole.
            // Private key information should be funged.
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            _rsa.ImportParameters(rsaParameters);
            byte[] signatureBytes = _rsa.SignData(dataBytes, _sha256);
            return Convert.ToBase64String(signatureBytes);
        }

        public static bool Verify(
            string data, string signature, RSAParameters rsaParameters)
        {
            byte[] dataBytes = Convert.FromBase64String(data);
            byte[] signatureBytes = Convert.FromBase64String(signature);

            _rsa.ImportParameters(rsaParameters);
            return _rsa.VerifyData(dataBytes, _sha256, signatureBytes);
        }

        public static RSAParameters ExtractRSAParameters(string publicKeyString)
        {
            RSAParameters parameters = new RSAParameters();

            string[] pairString = publicKeyString.Split(":");
            byte[][] pairBytes = {
                Convert.FromBase64String(pairString[0]),
                Convert.FromBase64String(pairString[1])};
            parameters.Modulus = pairBytes[0];
            parameters.Exponent = pairBytes[1];
            return parameters;
        }

        public static string GenerateNonce()
        {
            byte[] nonce = new byte[NONCE_LENGTH];
            _rng.GetBytes(nonce);
            return Convert.ToBase64String(nonce);
        }
    }
}
