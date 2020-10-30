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
        public By PhoneSaveButton = By.XPath("//div[@class='modal-location__body']");
        public By OperatorCodesDropDownExpander = By.XPath("//div[contains(@class, 'phone-item-select__item-codes')]");
        public By OperatorCodesDropDown = By.XPath("//div[contains(@class, ' css-11unzgr')]");
        public By DateOfBirth = By.XPath("//input[@placeholder='DD-MM-YYYY']");
        public By Sex = By.XPath("//div[@class='modal-location__body']");
        public By SubmitCheckbox = By.XPath("//div[@class='modal-location__body']");
        public By SubmitButton = By.XPath("//div[@class='modal-location__body']");
       
        //public IWebElement SubmitElement => SubmitButton.GetElement();
    }
}
