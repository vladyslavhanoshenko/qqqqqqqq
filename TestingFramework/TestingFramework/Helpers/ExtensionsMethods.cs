﻿using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace TestingFramework.Helpers
{
    static class ExtensionsMethods
    {
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
