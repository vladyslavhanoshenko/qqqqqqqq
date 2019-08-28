using System;
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
    }
}
