using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaAuctions.Models
{
    public class Content
    {
        public static string RoleUser = "0";
        public static string StatusLogin = "";
        public static User userinformation = null;
        public static string ran = "";
        private static Random random = new Random();

        public static string getDate(DateTime date)
        {
            string day = date.Day.ToString();
            string mon = date.Month.ToString();
            string year = date.Year.ToString();
            return day + "/" + mon + "/" + year;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}