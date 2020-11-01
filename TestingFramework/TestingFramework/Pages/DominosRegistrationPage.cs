using OpenQA.Selenium;

namespace TestingFramework.Pages
{
    public class DominosRegistrationPage
    {
        public PhoneNumberConfirmationPopup phoneNumberConfirmationPopup = new PhoneNumberConfirmationPopup(); 
        public const string OperatorCodesXpath = "//div[contains(@id, 'react-select')]";
        public By FirstName = By.XPath("//input[@name='first_name']");
        public By LastName = By.XPath("//input[@name='last_name']");
        public By Email = By.XPath("//input[@name='email']");
        public By Phone = By.XPath("//input[@name='number']");
        public By PhoneSaveButton = By.XPath("//div[contains(@class, 'phone-item-button-text')]");
        public By OperatorCodesDropDownExpander = By.XPath("//div[contains(@class, 'phone-item-select__item-codes')]");
        public By OperatorCodesDropDown = By.XPath("//div[contains(@class, ' css-11unzgr')]");
        public By DateOfBirth = By.XPath("//div[contains(@class, 'react-datepicker')]//input");
        public By SubmitCheckbox = By.XPath("//div[contains(@class, 'dp-checkbox')]//span[contains(@class, 'dp-checkbox__icon')]");
        public By SubmitButton = By.XPath("//div[contains(@class, 'order-button-wrap')]");
    }
}
