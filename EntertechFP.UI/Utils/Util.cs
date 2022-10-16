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
        public static string TimeAgo(DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} saniye önce", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("{0} dakika önce", timeSpan.Minutes) :
                    "1 saniye önce";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("{0} saat önce", timeSpan.Hours) :
                    "1 saat önce";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("{0} gün önce", timeSpan.Days) :
                    "dün";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("{0} ay önce", timeSpan.Days / 30) :
                    "1 ay önce";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("{0} yıl önce", timeSpan.Days / 365) :
                    "1 yıl önce";
            }

            return result;
        }
    }
}
