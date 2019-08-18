using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestingFramework.Helpers;
using TestingFramework.Pages;
using System;
using System.Net.Http;
using System.IO;
using TestingFramework.Tests.WordPress;
using System.Net;
using RestSharp;
using System.Collections.Generic;

namespace TestingFramework
{
    [TestFixture]
    public class WordPressLoginAndVerify
    {
        ExcelFileReader ExcelReader = new ExcelFileReader();
        ReadersAndWritersToOrFromFile WriteAndReadHelper = new ReadersAndWritersToOrFromFile();
        WordPressAdminLoginPage LoginPage = new WordPressAdminLoginPage();
        string indexFileReaderPath = @"F:\TestingFramework\IndexFileReader.txt";
        string indexFileWriterPath = @"F:\TestingFramework\IndexFileReader.txt";
        string Url;
        string Login;
        string Password;

        IWebDriver driver;

        [SetUp]
        public void DriverInitialization()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-extensions"); // to disable extension
            options.AddArguments("--disable-notifications"); // to disable notification
            options.AddArguments("--disable-application-cache"); // to disable cache

            driver = new ChromeDriver(@"F:\TestingFramework", options);
        }

        public void WriteIndexToTheFile(int index)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(indexFileWriterPath, false, System.Text.Encoding.Default))
                {
                    sw.Write(index);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Index was not saved");
            }
        }

        
        

        public string CheckForNotAcceptableError()
        {
            try
            {
                var notAcceptable = driver.FindElement(By.XPath("html/body/h1[contains(text(),'Not Acceptable!')]"));
                if (notAcceptable.Displayed)
                {
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return null;
        }

        public string CheckForPageNotFoundError()
        {
            try
            {
                var pageNotFoundError = driver.FindElement(By.XPath("//span[@class='status-code'][contains(text(),'404')]"));
                if (pageNotFoundError.Displayed)
                {
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return null;
        }

        public string CheckForDomainError()
        {
            

            try
            {
                var pageNotFoundError = driver.FindElement(By.XPath("//p[contains(text(),'This domain has expired.')]"));
                if (pageNotFoundError.Displayed)
                {
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return null;
        }

        public string CheckFor2PageNotFoundError()
        {
            try
            {
                var pageNotFoundError = driver.FindElement(By.XPath("//h1[contains(text(),'404 Not Found')]"));
                if (pageNotFoundError.Displayed)
                {
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return null;
        }

        [Test]
        public void LoginAndVerifyAsync()
        {
            var dataFromExcelFile = ExcelReader.ReadExcelFile();


            int indexFromFile = WriteAndReadHelper.ReadIndexFromFile(indexFileReaderPath);
            dataFromExcelFile.RemoveRange(0, indexFromFile);
            
                foreach (var field in dataFromExcelFile)
                {
                    var test2 = field.Split(';');
                    Url = "http://" + test2[0];
                    Login = test2[1];
                    Password = test2[2];
                    WriteIndexToTheFile(indexFromFile++);

                var client = new RestClient(Url);
                var request = new RestRequest(Method.GET);
                var test22 = client.Execute(request);
                if (!test22.StatusCode.Equals(HttpStatusCode.OK))
                {
                    continue;
                }
                




                driver.Navigate().GoToUrl(Url);

                if (driver.Url.Contains("robotcaptcha"))
                {
                    continue;
                }

                var isDomainErrorsPresent = CheckForDomainError();
                var isPageNotFoundErrorPresent = CheckForPageNotFoundError();
                    var isNotAcceptrableErrorPresent = CheckForNotAcceptableError();
                    var isPage2NotFoundErrorPresent = CheckFor2PageNotFoundError();
                    if (isPageNotFoundErrorPresent == "1" || isNotAcceptrableErrorPresent == "1" || isPage2NotFoundErrorPresent == "1" || isDomainErrorsPresent == "1")
                    {
                        continue;
                    }
                    if (driver.Url.Contains("suspendedpage"))
                    {
                        continue;
                    }

                    try
                    {
                        var pageNotLoaded = driver.FindElement(By.XPath("//div[@id='main-message']"));

                        if (pageNotLoaded.Displayed)
                        {
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                    }

                    try
                    {
                        var wordPressSuggestion = driver.FindElement(By.XPath("//a[@class='jetpack-sso-toggle wpcom']"));
                        if (wordPressSuggestion.Displayed)
                        {
                            wordPressSuggestion.Click();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                    }

                    try
                    {
                        var technicalDifficulties = driver.FindElement(By.XPath("//body[@id='error-page']/p[contains(text(), 'The site is experiencing technical difficulties. Please check your site admin email inbox for instructions.')]"));
                        if (technicalDifficulties.Displayed)
                        {
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                    }

                    try
                    {
                        var forbidden = driver.FindElement(By.XPath("html/body/h1[contains(text(),'Forbidden')]"));
                        if (forbidden.Displayed)
                        {
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                    }



                    var loginField = driver.FindElement(By.XPath("//input[@id='user_login']"));
                    var passwordField = driver.FindElement(By.XPath("//input[@id='user_pass']"));
                    var loginButton = driver.FindElement(By.XPath("//input[@id='wp-submit']"));

                    loginField.SendKeys(Login);
                    passwordField.SendKeys(Password);
                    loginButton.Click();

                    isNotAcceptrableErrorPresent = CheckForNotAcceptableError();
                    isPageNotFoundErrorPresent = CheckForPageNotFoundError();
                    isPage2NotFoundErrorPresent = CheckFor2PageNotFoundError();
                    if (isPageNotFoundErrorPresent == "1" || isNotAcceptrableErrorPresent == "1" || isPage2NotFoundErrorPresent == "1")
                    {
                        continue;
                    }

                    if (!driver.Url.Contains("wp-admin"))
                    {
                        var loginError = driver.FindElement(By.XPath("//div[@id='login_error']"));

                        if (loginError.Text.Contains("ERROR: Incorrect username or password") || loginError.Text.Contains("ERROR: Invalid username"))
                        {
                            continue;
                        }
                        if (loginError.Displayed)
                        {
                            driver.Navigate().Refresh();
                        }

                    }

                    string text = null;
                    string readPath = @"F:\TestingFramework\LoginsAndPasswords.txt";
                    string writePath = @"F:\TestingFramework\LoginsAndPasswords.txt";
                    if (driver.Url.Contains("wp-admin"))
                    {
                        string newUrl = $"{Url};{Login};{Password}";
                        try
                        {
                            var resultOfFileChecking = WriteAndReadHelper.WriteToFile(text, newUrl, readPath, writePath);
                            if (resultOfFileChecking == "Url is already present")
                            {
                                continue;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }





                }
            //}

            

        }
    }
}
