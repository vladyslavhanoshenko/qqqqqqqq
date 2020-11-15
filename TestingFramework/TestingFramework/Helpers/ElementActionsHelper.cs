﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TestingFramework.Commons;

namespace TestingFramework.Helpers
{
    public static class ElementActionsHelper
    {
        public static bool ContainsTag(this IWebElement element, string tagName)
        {
            string elementText = element.GetAttribute("innerHTML");
            return CheckStringForTag(elementText, tagName);
        }

        public static bool ContainsClass(this IWebElement element, string className)
        {
            string elementText = element.GetAttribute("innerHTML");
            return CheckStringForClass(elementText, className);
        }

        public static bool ContainsTag(this IWebDriver driver, string tagName)
        {
            return CheckStringForTag(driver.PageSource, tagName);
        }

        public static bool ContainsClass(this IWebDriver driver, string className)
        {
            return CheckStringForClass(driver.PageSource, className);
        }

        private static bool CheckStringForTag(string text, string tagName)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text.Contains("<" + tagName + ">") || text.Contains("</" + tagName + ">") || text.Contains("<" + tagName + " ");
            }
            return false;
        }

        private static bool CheckStringForClass(string text, string className)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                string pattern = string.Format(".*class[\\s]?=[\\s]?.*[\\s'\"]{0}[\\s'\"].*.*", className);
                Match m = Regex.Match(text, className, RegexOptions.IgnoreCase);
                return m.Success;
            }
            return false;
        }

        public static string GetInnerHTML(this IWebElement element)
        {
            return element.GetAttribute("innerHTML");
        }

        public static IWebElement GetElement(this By by)
        {
            return Driver.driver.FindElement(by);
        }

        public static bool ExistsFast(this By elementSearchConfiguration, int tries = 2)
        {
            var oldImplicitWaitsTime = Driver.driver.Manage().Timeouts().ImplicitWait;
            Driver.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            while (tries > 0)
            {
                try
                {
                    var isDisplayed = elementSearchConfiguration.GetElement().Displayed;
                    Driver.driver.Manage().Timeouts().ImplicitWait = oldImplicitWaitsTime;
                    return isDisplayed;
                }
                catch (Exception e)
                {
                    tries--;
                }
            }
            Driver.driver.Manage().Timeouts().ImplicitWait = oldImplicitWaitsTime;
            return false;
        }

        public static void WaitUntilElementIsNotDisplayed(this By elementSearchConfiguration, int secondsForWait = 5)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            bool isElementExist = false;

            while (sw.ElapsedMilliseconds < secondsForWait * 1000)
            {
                try
                {
                    isElementExist = elementSearchConfiguration.ExistsFast();
                }
                catch (Exception e)
                {

                }
            }
            if (isElementExist)
                throw new Exception("Element is displayed");
        }

        public static bool Exists(this By by, int tries = 2)
        {
            var oldImplicitWaitsTime = Driver.driver.Manage().Timeouts().ImplicitWait;
            Driver.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            while (tries >= 0)
            {
                try
                {
                    var isDisplayed = Driver.driver.FindElement(by).Displayed;
                    Driver.driver.Manage().Timeouts().ImplicitWait = oldImplicitWaitsTime;
                    return isDisplayed;
                }
                catch (Exception e)
                {
                    tries--;
                }
            }
            return false;
        }

        //public static void WaitUntilElementIsNotDisplayed(this By elementSearchConfiguration)
        //{
        //    var wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, 5));

        //    wait.Until(condition =>
        //    {
        //        try
        //        {
        //            var elementToBeDisplayed = elementSearchConfiguration.GetElement().Displayed;
        //            return false;
        //        }
        //        catch (StaleElementReferenceException)
        //        {
        //            return false;
        //        }
        //        catch (NoSuchElementException)
        //        {
        //            return false;
        //        }
        //    });
        //    //bool isDisplayed = true;
        //    //while (isDisplayed == true)
        //    //{
        //    //    wait.Until(condition =>
        //    //    {
        //    //        try
        //    //        {
        //    //            var elementToBeDisplayed = elementSearchConfiguration.GetElement().Displayed;
        //    //            return elementToBeDisplayed;
        //    //        }
        //    //        catch (StaleElementReferenceException)
        //    //        {
        //    //            isDisplayed = true;
        //    //        }
        //    //        catch (NoSuchElementException)
        //    //        {
        //    //            isDisplayed = false;
        //    //            return true;
        //    //        }
        //    //    });
        //    //}
        //}



        public static string ToMd5Hash(this string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
