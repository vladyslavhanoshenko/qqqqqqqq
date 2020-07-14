using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Web.UI.HtmlControls;

namespace TestingFramework.Commons
{
    public class BasePage
    {
        public IWebDriver driver;
        public WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //public void InitPage<T>(T pageClass) where T : BasePage
        //{
        //    PageFactory.InitElements(driver, pageClass);
        //}
    }
}
