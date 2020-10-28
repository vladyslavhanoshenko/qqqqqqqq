
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestingFramework.Commons;
using TestingFramework.Helpers;
using TestingFramework.Steps;

namespace TestingFramework.Pages
{
    public class DominosContext : BaseSteps
    {
        private DominosLoginPage _mainPage;
        public DominosRegistrationPageSteps dominosRegistrationPageSteps= new DominosRegistrationPageSteps();
        public PhoneNumberConfirmationPopupSteps phoneNumberConfirmationPopupSteps = new PhoneNumberConfirmationPopupSteps();
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

        public void AddPhoneNumber(string phoneNumber)
        {
            var operatorCode = phoneNumber.Substring(0, 5);
            var numberWithoutOperatorCode = phoneNumber.Substring(5);
            dominosRegistrationPageSteps.SelectOperatorCode(operatorCode);
            dominosRegistrationPageSteps.SetPhoneNumber(numberWithoutOperatorCode);
            dominosRegistrationPageSteps.ClickPhoneSaveButton();
            phoneNumberConfirmationPopupSteps.WaitUntilSmsConfirmationPopupDisplayed();
            phoneNumberConfirmationPopupSteps.SetSmsCode();
            phoneNumberConfirmationPopupSteps.ClickSmsConfirmationButton();
            phoneNumberConfirmationPopupSteps.WaitUntilSmsConfirmationPopupIsNotDisplayed();
        }
        public void VerifyAccont(string url)
        {
            dominosRegistrationPageSteps.OpenConfirmationLink(url);
            dominosRegistrationPageSteps.WaitUntilRegistrationPageOpened();
            dominosRegistrationPageSteps.SetFirstName();
            dominosRegistrationPageSteps.SetLastName();
            dominosRegistrationPageSteps.SetEmail();
            AddPhoneNumber();
            
            dominosRegistrationPageSteps.SetDateOfBirth();
            dominosRegistrationPageSteps.SelectSex();
            dominosRegistrationPageSteps.CheckConfirmationCheckbox();
            dominosRegistrationPageSteps.ClickConfirmationButton();
            

        }
    }
}
