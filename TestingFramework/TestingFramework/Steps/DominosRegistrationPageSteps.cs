﻿using TestingFramework.Commons;
using TestingFramework.Helpers;
using TestingFramework.Pages;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace TestingFramework.Steps
{
    public class DominosRegistrationPageSteps : DominosRegistrationPageStepsBase
    {
        public void OpenConfirmationLink(string url)
        {
            Driver.driver.Navigate().GoToUrl(url);
        }

        public void WaitUntilRegistrationPageOpened()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(registrationPage.FirstName));
        }

        public void SetFirstName(string firstName)
        {
            registrationPage.FirstName.GetElement().SendKeys(firstName);
        }

        public void SetLastName(string lastName)
        {
            registrationPage.LastName.GetElement().SendKeys(lastName);
        }

        public void SetEmail(string email)
        {
            registrationPage.Email.GetElement().SendKeys(email);
        }

        public void OpenOperatorCodeDropDown()
        {
            registrationPage.OperatorCodesDropDownExpander.GetElement().Click();
        }

        public void SelectOperatorCode(string operatorCode)
        {
            var expandButton = Driver.driver.FindElement(By.XPath("//div[contains(@class, 'indicatorContainer')]"));
            expandButton.Click();
            var dropDownElements = Driver.driver.FindElements(By.XPath(DominosRegistrationPage.OperatorCodesXpath)).ToList();
            var operatorCodeItem = dropDownElements.Single(i => i.GetInnerHTML().Equals(operatorCode));
            operatorCodeItem.Click();
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            registrationPage.Phone.GetElement().SendKeys(phoneNumber);
        }

        public void ClickPhoneSaveButton()
        {
            registrationPage.PhoneSaveButton.GetElement().Click();
        }

        public void SetDateOfBirth(string dateOfBirth)
        {
            registrationPage.DateOfBirth.GetElement().SendKeys(dateOfBirth);
        }

        public void SelectSex(string sex)
        {
            var dropDownElements = Driver.driver.FindElements(By.XPath("//input[@name='gender']/..//div[@class='dp-select__drop-down']/div")).ToList();
            var itemToBeSelected = dropDownElements.Single(i => i.Equals(sex));
            itemToBeSelected.Click();
        }

        public void CheckConfirmationCheckbox()
        {
            registrationPage.SubmitCheckbox.GetElement().Click();
        }

        public void ClickConfirmationButton()
        {
            registrationPage.SubmitButton.GetElement().Click();
        }
    }
}
