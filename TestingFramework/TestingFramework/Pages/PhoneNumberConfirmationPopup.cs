using OpenQA.Selenium;

namespace TestingFramework.Pages
{
    public class PhoneNumberConfirmationPopup
    {
        public By SmsCodeInput = By.XPath("//input[@name='code']");
        public By ConfirmButton = By.XPath("//button[@type='submit']");
    }
}
