using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;

namespace TestingFramework.Pages
{
    class WordPressAdminLoginPage
    {
        public const string LOGIN_INPUT = "//input[@id='user_login']";
        public const string PASSWORD_INPUT = "//input[@id='user_pass']";
        public const string LOGIN_BUTTON = "//input[@id='wp-submit']";

        [FindsBy(How = How.XPath, Using = LOGIN_INPUT)]
        public IWebElement loginTextField;

        [FindsBy(How = How.XPath, Using = PASSWORD_INPUT)]
        public IWebElement passwordTextField;

        [FindsBy(How = How.XPath, Using = LOGIN_BUTTON)]
        public IWebElement loginButton;

        public WordPressAdminLoginPage Login()
        {
            //loginTextField.SendKeys(login);
            //passwordTextField.SendKeys(password);
            //loginButton.Click();
            return this;
        }
    }
}
