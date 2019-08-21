using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DocumentLight.System.Helpers
{
    public static class HashHelper
    {
        public static string GetHash(string input)
        {
            var buffer = Encoding.UTF8.GetBytes(input);
            string output = BitConverter.ToString(new HMACSHA256().ComputeHash(buffer)).Replace("-", "");
            return output;
        }
    }
}
