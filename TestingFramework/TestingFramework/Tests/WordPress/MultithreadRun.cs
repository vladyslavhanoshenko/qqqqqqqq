using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System;
using TestingFramework.Helpers;
using TestingFramework.Pages;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System.Net;
using System.IO;

namespace TestingFramework.Tests.WordPress
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    class MultithreadRun
    {
        WordPressLoginAndVerify runner = new WordPressLoginAndVerify();
        ExcelFileReader ExcelReader = new ExcelFileReader();
        ReadersAndWritersToOrFromFile WriteAndReadHelper = new ReadersAndWritersToOrFromFile();
        WordPressAdminLoginPage LoginPage = new WordPressAdminLoginPage();
        string indexFileReaderPath = @"F:\qqqqqqqq\TestingFramework\IndexFileReader.txt";
        string indexFileWriterPath = @"F:\qqqqqqqq\TestingFramework\IndexFileReader.txt";
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

            driver = new ChromeDriver(@"F:\qqqqqqqq\TestingFramework", options);
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

        


        }

    }

