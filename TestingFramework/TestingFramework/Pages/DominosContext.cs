
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestingFramework.Helpers;

namespace TestingFramework.Pages
{
    public class DominosContext
    {
        private DominosLoginPage _mainPage;
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        public DominosContext(IWebDriver driver)
        {
           
            _mainPage = new DominosLoginPage(driver);
        }

        public void RegisterAccount(string email, string password)
        {
            _mainPage.OpenMainPage();

            if (_mainPage.LocationSelectPopUp.Exists())
            {
                _mainPage.LocationSelectPopUpCloseButton.GetElement().Click();
            }
            _mainPage.OpenLoginPopupButton.GetElement().Click();
            _mainPage.RegisterButton.GetElement().Click();
            _mainPage.EmailField.GetElement().SendKeys(email);
            _mainPage.PasswordField.GetElement().SendKeys(password);
            _mainPage.PasswordField2.GetElement().SendKeys(password);
            _mainPage.SubmitRegistrationButton.GetElement().Click();
        }
    }
}
