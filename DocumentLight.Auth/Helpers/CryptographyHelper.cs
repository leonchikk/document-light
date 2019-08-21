using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DocumentLight.Auth.Helpers
{
    internal static class CryptographyHelper
    {
        public static string EncryptPswd(string input)
        {
            var buffer = Encoding.UTF8.GetBytes(input);
            string output = BitConverter.ToString(new SHA512Managed().ComputeHash(buffer)).Replace("-", "");
            return output;
        }
    }
}
