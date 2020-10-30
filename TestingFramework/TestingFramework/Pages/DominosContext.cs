
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestingFramework.Commons;
using TestingFramework.Helpers;
using TestingFramework.Services.REST.SmsRegApi;
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
            wait.Until(ExpectedConditions.ElementIsVisible(_mainPage.RegisterButton));
            Actions action = new Actions(Driver.driver);
            action.MoveToElement(_mainPage.RegisterButton.GetElement()).Perform();
            
            _mainPage.RegisterButton.GetElement().Click();
            _mainPage.EmailField.GetElement().SendKeys(email);
            _mainPage.PasswordField.GetElement().SendKeys(password);
            _mainPage.PasswordField2.GetElement().SendKeys(password);
            _mainPage.SubmitRegistrationButton.GetElement().Click();
        }

        public void AddPhoneNumber()
        {
            var availableNumbers = SmsRegApiService.GetAvailableNumberForOtherServices();
            if (availableNumbers > 0)
            {
                //add waiter for numbers
            }
            var activationData = SmsRegApiService.GetNumberForOtherService();
            var operatorCode = activationData.PhoneNumber.Substring(0, 5).Remove(0, 2);
            var numberWithoutOperatorCode = activationData.PhoneNumber.Remove(0, 5);
            SmsRegApiService.SetStatus(8, activationData.Id, 0);
            dominosRegistrationPageSteps.SelectOperatorCode(operatorCode);
            dominosRegistrationPageSteps.SetPhoneNumber(numberWithoutOperatorCode);
            dominosRegistrationPageSteps.ClickPhoneSaveButton();
            phoneNumberConfirmationPopupSteps.WaitUntilSmsConfirmationPopupDisplayed();
            phoneNumberConfirmationPopupSteps.SetSmsCode(null);
            phoneNumberConfirmationPopupSteps.ClickSmsConfirmationButton();
            phoneNumberConfirmationPopupSteps.WaitUntilSmsConfirmationPopupIsNotDisplayed();
        }
        public void VerifyAccont(string url)
        {
            dominosRegistrationPageSteps.OpenConfirmationLink(url);
            dominosRegistrationPageSteps.WaitUntilRegistrationPageOpened();
            dominosRegistrationPageSteps.SetFirstName("Vladyslav");
            dominosRegistrationPageSteps.SetLastName("Petrov");
            AddPhoneNumber();
            
            dominosRegistrationPageSteps.SetDateOfBirth(null);
            dominosRegistrationPageSteps.SelectSex(null);
            dominosRegistrationPageSteps.CheckConfirmationCheckbox();
            dominosRegistrationPageSteps.ClickConfirmationButton();
            

        }
    }
}
