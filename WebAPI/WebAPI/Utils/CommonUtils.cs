using Domain.Entities;
using Domain.Models;

namespace WebAPI.Utils
{
    public class CommonUtils
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            base64EncodedData = Uri.UnescapeDataString(base64EncodedData);// base64EncodedData.Replace("%3D", "=");
                                                                          // base64EncodedData = base64EncodedData.Replace("%2F", "/");
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string getFullName(User usr)
        {
            if (usr == null)
            {
                return "";
            }
            return (usr?.FirstName?.Trim() + " " + usr?.LastName?.Trim()).Replace("  ", " ");
        }
    }
}
