using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestingFramework.Commons;

namespace TestingFramework.Pages.Sms_activate
{
    public class MainPage : BasePage
    {
        public IWebElement loginAndRegButton { get
            {
                return driver.FindElement(By.XPath("//a[contains(text(), 'Вход/Регистрация')]"));
            } }

        public IWebElement emailTextField { get
            {
                return driver.FindElement(By.XPath("//form[@id='loginForm']//input[@name='email']"));
            } }
        public IWebElement passwordField
        {
            get
            {
                return driver.FindElement(By.XPath("//form[@id='loginForm']//input[@name='pass']"));
            }
        }

        public IWebElement LoginButton
        {
            get
            {
                return driver.FindElement(By.XPath("//form[@id='loginForm']//input[@name='submit']"));
            }
        }

        public IWebElement AnyServiceNumber
        {
            get
            {
                return driver.FindElement(By.XPath("(//span[contains(text(), 'Любой другой')])[2]"));
            }
        }
        //[FindsBy(How = How.XPath, Using = "//form[@id='leftForm']/table/tbody")]
        //public NumbersPanel NumbersPanel { get; set; }
        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> GetAllTableNumbers() => driver.FindElements(By.XPath("//form[@id='leftForm']/table/tbody/tr"));

        public MainPage(IWebDriver driver) : base(driver)
        {

            //PageFactory.InitElements(driver, NumbersPanel);
        }
        
        //public IWebElement[] GetServicesList()
        //{
        //    driver.FindElements(By.XPath(""));
        //}]
    }
}
