using System.Text;
using System.Security.Cryptography;

namespace EntertechFP.API.Utils
{
    public class Util
    {
        public static string HashToMD5(string text)
        {
            using (MD5CryptoServiceProvider md5 = new())
            {
                byte[] array = Encoding.UTF8.GetBytes(text);
                array = md5.ComputeHash(array);
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte item in array)
                {

                    stringBuilder.Append(item.ToString("x2").ToLower());
                }
                return stringBuilder.ToString();
            }
        }
        public static string CreateApiKey() => Guid.NewGuid().ToString();
    }
}
