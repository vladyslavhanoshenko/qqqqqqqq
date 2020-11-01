using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingFramework.Commons;
using TestingFramework.Helpers;
using TestingFramework.Models;
using TestingFramework.Pages;
using TestingFramework.Services.REST.SmsRegApi;
using TestingFramework.Services.REST.TempMailOrg;

namespace TestingFramework.Tests.Dominos
{
    [TestFixture]
    public class AccountReg:BaseTest
    {
        public static IWebDriver driver;
        //IWebDriver incognitoBroswer = new ChromeDriver(driverPath);

        
        public string SmsActivateBaseUrl = "https://sms-activate.ru/ru/";
        public string Login = "vladyslavhanoshenko@gmail.com";
        //public string Password = "kzARnxTW25ASb-r";
        
        string driverPath = @"F:\qqqqqqqq\TestingFramework\TestingFramework";

        public string excelFileSavePath = @"F:\dominosaccounts.xlsx";
        private Random random = new Random();
        private int numForEmail => random.Next(10000);
        private string MailBoxName => $"petrov{numForEmail}";
  
        private const string Password = "qwerty67u9";
        //private string FullEmailAddress => MailBoxName + "@" + MailBoxDomain;
        private TempMailApi tempMailApi = new TempMailApi();

        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> Wait()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='modal-dialog modal-lg']//div[@class='modal-content']")));
        }
        
        //[SetUp]
        //public void DriverSetUp()
        //{
        //    ChromeOptions options = new ChromeOptions();
        //    //options.AddArguments("--incognito");
        //    options.AddArguments("--disable-notifications");
        //    options.AddArguments("--disable-geolocation");
        //    options.AddArguments("--disable-extensions");
        //    options.AddArguments("start-maximized");
        //    options.AddArguments("disable-infobars");
           
        //    driver = new ChromeDriver(driverPath, options);
        //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        //    Driver.driver = driver;
        //}

        public void DriverSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            options.AddArguments("--disable-geolocation");
            options.AddArguments("--disable-extensions");
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");

            driver = new ChromeDriver(driverPath, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Driver.driver = driver;
        }


        
        
        [Test]
        public void DominosReg()
        {
            var dominosCreatedAccounts = new List<DominosCreatedAccountEntity>();

            try
            {
                

                for (int i = 0; i < 20; i++)
                {
                    DriverSetup();
                    var domainNamesList = tempMailApi.GetDomainsList();
                    var fullEmailAddress = MailBoxName + domainNamesList.First();


                    DominosCreatedAccountEntity accountData = new DominosCreatedAccountEntity
                    {
                        Email = fullEmailAddress,
                        FirstName = "Vladyslav",
                        LastName = "Petrov",
                        Sex = "Чоловік"
                    };

                    var numberOfProperties = accountData.GetType().GetProperties();

                    DominosContext dominosContext = new DominosContext(driver);
                    dominosContext.RegisterAccount(fullEmailAddress, Password);
                    var emailText = tempMailApi.GetMailsWithWait(fullEmailAddress.ToMd5Hash());
                    var verificationLink = emailText.Single().MailText.GetDominosUrl();
                    dominosContext.VerifyAccont(verificationLink, accountData);
                    Driver.driver.Quit();

                    dominosCreatedAccounts.Add(accountData);
                }

                ExcelFileHelpers.WriteDominosAccountsDataToExcel(excelFileSavePath, dominosCreatedAccounts.ToArray());

            }
            catch(Exception e)
            {
                ExcelFileHelpers.WriteDominosAccountsDataToExcel(excelFileSavePath, dominosCreatedAccounts.ToArray());
                Console.WriteLine(e.Message);

            }

            
        }

        //[TearDown]
        //public void SmsActivateTearDown()
        //{
        //    driver.Quit();
        //}
    }
}
