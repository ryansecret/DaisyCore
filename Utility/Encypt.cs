using System;
using System.Security.Cryptography;
using System.Text;
using Daisy.Core.Config;

namespace Daisy.Core.Utility
{
    public class Encypt
    {
        public static string HashPassWord(string message,string salt)
        {
           
            
            var md5 = new MD5CryptoServiceProvider();
        
            var byt = Encoding.UTF8.GetBytes(salt + message);
            var bytHash = md5.ComputeHash(byt);
            md5.Clear();
            var sTemp = "";
            for (var i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
            }
            return sTemp;
        }

        public static string GetGuid()
        {
            var bytes=new byte[16];
            RandomNumberGenerator.Create().GetBytes(bytes);
            Guid guid=new Guid(bytes);
            return guid.ToString();
        }
    }
}