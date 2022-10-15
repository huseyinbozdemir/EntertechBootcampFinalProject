using System.Text;
using System.Security.Cryptography;

namespace EntertechFP.UI.Utils
{
    public class Util
    {
        public static string HashToMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
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
}
