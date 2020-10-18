using OpenQA.Selenium;
using TestingFramework.Commons;

namespace TestingFramework.Pages
{
    public class DominosLoginPage : BasePage
    {
        public string DominosBaseUrl = "https://dominos.ua/";
       
        public DominosLoginPage(IWebDriver driver) : base(driver) { }
        public By LocationSelectPopUp = By.XPath("//div[@class='modal-location__body']");
        public By LocationSelectPopUpCloseButton = By.XPath("//button[@class='dp-modal__close-btn']");
        public By OpenLoginPopupButton = By.XPath("//div[@class='fake-header__sing-in-block']/button");
        public By LoginPopup = By.XPath("//div[@class='dp-modal__body']");
        public By RegisterButton = By.XPath("//button[contains(text(), 'Регистрация') or contains(text(), 'Реєстрація')]");
        public By EmailField = By.XPath("//input[@placeholder='Ваш email']");
        public By PasswordField = By.XPath("//input[@name='password_1']");
        public By PasswordField2 = By.XPath("//input[@name='password_2']");
        public By SubmitRegistrationButton = By.XPath("//button[contains(text(), 'Зарегистрироваться') or contains(text(), 'Зареєструватись')]");
        public By SuccesfulRegPopu = By.XPath("//h1[contains(text(), 'Ссылка на вход была отправлена на ваш email') or contains(text(), 'Посилання на вхід було відправлено на вашу email адресу')]");
        //public IWebElement LocationSelectPopUp { get
        //    {
        //        return driver.FindElement(By.XPath("//div[@class='modal-location__body']"));
        //    } }

        //public IWebElement LocationSelectPopUpCloseButton { get
        //    {
        //        return driver.FindElement(By.XPath("//button[@class='dp-modal__close-btn']"));
        //    } }

        //public IWebElement OpenLoginPopupButton { get
        //    {
        //        return driver.FindElement(By.XPath("//div[@class='fake-header__sing-in-block']/button"));
        //    } }
        
        //public IWebElement LoginPopup { get
        //    {
        //        return driver.FindElement(By.XPath("//div[@class='dp-modal__body']"));
        //    } }
        
        //public IWebElement RegisterButton { get
        //    {
        //        return driver.FindElement(By.XPath("//button[contains(text(), 'Регистрация')]"));
        //    } }
        
        //public IWebElement EmailField { get
        //    {
        //        return driver.FindElement(By.XPath("//input[@placeholder='Ваш email']"));
        //    } }

        //public IWebElement PasswordField
        //{
        //    get
        //    {
        //        return driver.FindElement(By.XPath("//input[@name='password_1']"));
        //    }
        //}
        
        //public IWebElement PasswordField2
        //{
        //    get
        //    {
        //        return driver.FindElement(By.XPath("//input[@name='password_2']"));
        //    }
        //}
        
        //public IWebElement SubmitRegistrationButton
        //{
        //    get
        //    {
        //        return driver.FindElement(By.XPath("//button[contains(text(), 'Зарегистрироваться')]"));
        //    }
        //}
        
        //public IWebElement SuccesfulRegPopu
        //{
        //    get
        //    {
                
        //        return driver.FindElement(By.XPath("//h1[contains(text(), 'Ссылка на вход была отправлена на ваш email')]"));
        //    }
        //}

        public void OpenMainPage()
        {
            driver.Navigate().GoToUrl(DominosBaseUrl);
        }
    }
}
