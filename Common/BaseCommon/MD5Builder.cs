using System;
using System.Security.Cryptography;
using System.Text;

namespace BaseCommon
{
    public class MD5Builder
    {
        public static string Builder32Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
