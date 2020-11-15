using System;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace TestingFramework.Helpers
{
    public static class ExtensionsMethods
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string chars = "zdasdfasfsdfsdvdfgbfhnggitrewrtfdffgvcxcnmbkjqeqwuiuigyiuzpppjnsd";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool IsNullOrEmpty(this IWebElement element)
        {
            if(element == null)
                return true;
            return false;
        }

        public static string GetDominosUrl(this string text)
        {
            string searchPattern = @"https://dominos.ua/uk/kyiv/user/verify/(\w*)";
            var regex = new Regex(searchPattern);
            bool isMatch = regex.IsMatch(text);
            var resultRegex = regex.Match(text);
            var result = regex.Match(text).Value;
            return result;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, false);
        }
    }
}
