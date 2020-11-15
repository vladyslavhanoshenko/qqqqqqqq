
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using TestingFramework.Commons;
using TestingFramework.Helpers;
using TestingFramework.Models;
using TestingFramework.Services.REST.SmsRegApi;
using TestingFramework.Steps;

namespace TestingFramework.Pages
{
    public class DominosContext : BaseSteps
    {
        private DominosLoginPage _mainPage;
        public DominosRegistrationPageSteps dominosRegistrationPageSteps= new DominosRegistrationPageSteps();
        public PhoneNumberConfirmationPopupSteps phoneNumberConfirmationPopupSteps = new PhoneNumberConfirmationPopupSteps();
        public DominosProfileSteps DominosProfileSteps = new DominosProfileSteps();
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        public DominosContext(IWebDriver driver)
        {
           
            _mainPage = new DominosLoginPage(driver);
        }

        public void RegisterAccount(string email, string password)
        {
            _mainPage.OpenMainPage();

            //if (_mainPage.LocationSelectPopUp.Exists())
            //{
            //    _mainPage.LocationSelectPopUpCloseButton.GetElement().Click();
            //}

            _mainPage.LoadingSpinner.WaitUntilElementIsNotDisplayed();
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

        public void OpenRegistrationPanel()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(_mainPage.RegisterButton));
            Actions action = new Actions(Driver.driver);
            action.MoveToElement(_mainPage.RegisterButton.GetElement()).Perform();
            _mainPage.RegisterButton.GetElement().Click();
            if (_mainPage.RegisterButton.Exists())
            {
                _mainPage.RegisterButton.GetElement().Click();
            }
            wait.Until(ExpectedConditions.ElementIsVisible(_mainPage.EmailField));
        }
        public void WaitForNumbersForOtherServices()
        {
            var availableNumbers = SmsRegApiService.GetAvailableNumberForOtherServices();

            while (availableNumbers < 1)
            {
                availableNumbers = SmsRegApiService.GetAvailableNumberForOtherServices();
            }
        }

        public string AddPhoneNumber()
        {
            WaitForNumbersForOtherServices();
            var activationData = SmsRegApiService.GetNumberForOtherService();
            var operatorCode = activationData.PhoneNumber.Substring(0, 5).Remove(0, 2);
            var numberWithoutOperatorCode = activationData.PhoneNumber.Remove(0, 5);
            
            dominosRegistrationPageSteps.SelectOperatorCode(operatorCode);
            dominosRegistrationPageSteps.SetPhoneNumber(numberWithoutOperatorCode);
            dominosRegistrationPageSteps.ClickPhoneSaveButton();
            phoneNumberConfirmationPopupSteps.WaitUntilSmsConfirmationPopupDisplayed();

            SmsRegApiService.SetStatus(1, activationData.Id, 0);

            string smsCode = null;
            var isSmsReceivedAfter3Min = SmsRegApiService.WaitUntilSmsTextIsReady(activationData.Id);
            if (isSmsReceivedAfter3Min)
            {
                var smsText = SmsRegApiService.GetSmsText(activationData.Id);
                var splittedText = smsText.Split(':');
                smsCode = splittedText.Last();
            }
            else
            {
                SmsRegApiService.SetStatus(8, activationData.Id, 0);
                WaitForNumbersForOtherServices();
                var activationDataSecondTry = SmsRegApiService.GetNumberForOtherService();
                var operatorCodeSecondTry = activationDataSecondTry.PhoneNumber.Substring(0, 5).Remove(0, 2);
                var numberWithoutOperatorCodeSecondTry = activationDataSecondTry.PhoneNumber.Remove(0, 5);
            }
            

            phoneNumberConfirmationPopupSteps.SetSmsCode(smsCode);
            phoneNumberConfirmationPopupSteps.ClickSmsConfirmationButton();
            phoneNumberConfirmationPopupSteps.WaitUntilSmsConfirmationPopupIsNotDisplayed();
            SmsRegApiService.SetStatus(6, activationData.Id, 0);

            return activationData.PhoneNumber;
        }

        public void VerifyAccont(string url, DominosCreatedAccountEntity accountData)
        {
            dominosRegistrationPageSteps.OpenConfirmationLink(url);
            dominosRegistrationPageSteps.WaitUntilRegistrationPageOpened();
            dominosRegistrationPageSteps.SetFirstName(accountData.FirstName);
            dominosRegistrationPageSteps.SetLastName(accountData.LastName);
            accountData.PhoneNumber = AddPhoneNumber();

            DateTime datetime = DateTime.Now.AddDays(29).AddYears(-25);
            var shortformat = datetime.ToShortDateString();
            accountData.Date = shortformat;

            dominosRegistrationPageSteps.SetDateOfBirth(accountData.Date);
            dominosRegistrationPageSteps.SelectSex(accountData.Sex);
            dominosRegistrationPageSteps.CheckConfirmationCheckbox();
            dominosRegistrationPageSteps.ClickConfirmationButton();
            dominosRegistrationPageSteps.WaitUntilRegistrationPageClosed();
            DominosProfileSteps.WaitUntilProfileSaveButtonDisplayed();
        }
    }
}
