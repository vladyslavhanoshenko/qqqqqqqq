using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestingFramework.Commons;
using TestingFramework.Helpers;

namespace TestingFramework.Steps
{
    public class PhoneNumberConfirmationPopupSteps : DominosRegistrationPageStepsBase
    {
        public void SetSmsCode(string smsCode)
        {
            registrationPage.phoneNumberConfirmationPopup.SmsCodeInput.GetElement().SendKeys(smsCode);
        }

        public void ClickSmsConfirmationButton()
        {
            registrationPage.phoneNumberConfirmationPopup.ConfirmButton.GetElement().Click();
        }

        public void WaitUntilSmsConfirmationPopupDisplayed()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(registrationPage.phoneNumberConfirmationPopup.ConfirmButton));
        }

        public void WaitUntilSmsConfirmationPopupIsNotDisplayed()
        {
            wait.Until(condition => 
            {
                var isElementDisplayed = registrationPage.phoneNumberConfirmationPopup.ConfirmButton.GetElement().Displayed;

                try
                {
                    var elementToBeDisplayed = registrationPage.phoneNumberConfirmationPopup.ConfirmButton.GetElement().Displayed;
                    return elementToBeDisplayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }
    }
}
