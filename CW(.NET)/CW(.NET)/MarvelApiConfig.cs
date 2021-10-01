using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CW_.NET_
{
    class MarvelApiConfig
    {
        public static string PublicKey => "228c38eb0e28f22a05c981c8170e1215";
        private static string PrivateKey => "f732b513e40f7c4c172d479eab5856cfd522a38c";
        public static string BaseURL => "http://gateway.marvel.com/v1/public/";
        public static long TimeStamp => DateTimeOffset.Now.ToUnixTimeSeconds();
        public static string Hash()
        {
            MD5 cryptor = MD5.Create();
            string stringToHash = $"{TimeStamp}{PrivateKey}{PublicKey}";
            byte[] bytes = cryptor.ComputeHash(Encoding.Default.GetBytes(stringToHash));
            return BitConverter
                .ToString(bytes)
                 .Replace("-", string.Empty)
                  .ToLower();
        }
    }
}