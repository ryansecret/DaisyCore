 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine(HashPassWord("adf", "1"));
            System.Console.Read();
        }

        public static string HashPassWord(string message, string salt)
        {
            var md5 = MD5.Create();
            var byt = Encoding.UTF8.GetBytes(salt + message);
            var bytHash = md5.ComputeHash(byt);
            md5.Dispose();
            var sTemp = "";
            for (var i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
            }
            return sTemp;
        }
    }
}
