﻿using NUnit.Framework;
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
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using OpenQA.Selenium.Support.PageObjects;

namespace TestingFramework
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class WordPressLoginAndVerify
    {
        ExcelFileHelpers ExcelReader = new ExcelFileHelpers();
        ReadersAndWritersToOrFromFile WriteAndReadHelper = new ReadersAndWritersToOrFromFile();

        ErrorsChecker ErrorChecker = new ErrorsChecker();
        string indexFileReaderPath = @"F:\qqqqqqqq\TestingFramework\IndexFileReader.txt";
        public string filePath = @"F:\qqqqqqqq\TestingFramework\all2.xlsx";
        string indexFileWriterPath = @"F:\qqqqqqqq\TestingFramework\IndexFileReader.txt";
        string driverPath = @"F:\qqqqqqqq\TestingFramework";
        string text = null;
        string readPath = @"F:\qqqqqqqq\TestingFramework\LoginsAndPasswords.txt";
        string writePath = @"F:\qqqqqqqq\TestingFramework\LoginsAndPasswords.txt";
        string newPathOfRewriteFile = @"F:\qqqqqqqq\TestingFramework\ToRewrite.txt";
        string newFileForLoginAndPasswords = @"F:\qqqqqqqq\TestingFramework\NewLogAndPass.txt";
        string Url;
        string Login;
        string Password;

        IWebDriver driver;

        //[SetUp]
        //public void DriverInitialization()
        //{
        //    ChromeOptions options = new ChromeOptions();
        //    options.AddArguments("--disable-extensions");
        //    options.AddArguments("--disable-notifications");
        //    options.AddArguments("--disable-application-cache");
        //    driver = new ChromeDriver(driverPath, options);
        //}

        //[TearDown]
        //public void DriverExit()
        //{
        //    driver.Close();
        //}

        [Test]
        public void LoginAndVerifyAsync()
        {
            if (!File.Exists(filePath) || !File.Exists(indexFileReaderPath) || !File.Exists(indexFileWriterPath) || !File.Exists(readPath) || !File.Exists(writePath))
                throw new FileNotFoundException("One of files listed above doesn't exists");
            var dataFromExcelFile = ExcelReader.ReadExcelFile(filePath);
            var dataFromTxt = WriteAndReadHelper.ReadFile(newPathOfRewriteFile).Split('\n').ToList();
            int indexFromFile = WriteAndReadHelper.ReadIndexFromFile(indexFileReaderPath);
            dataFromExcelFile.RemoveRange(0, indexFromFile);
            driver = new ChromeDriver(driverPath);
            foreach (var field in dataFromTxt) //изменить,если нужно читать с ткст файла или ексель
            {
                try
                {
                    var splittedArray = field.Split(';');
                    if (!splittedArray[0].Contains("http://"))
                        Url = "http://" + splittedArray[0];
                    else
                        Url = splittedArray[0];
                    if (Url.Contains("#NA"))
                        continue;
                    Login = splittedArray[1];
                    Password = splittedArray[2];
                    WriteAndReadHelper.WriteIndexToTheFile(indexFromFile++, indexFileWriterPath);
                    WriteAndReadHelper.RemoveFromFile(field, newPathOfRewriteFile);
                    RestClient client;
                    IRestResponse response = null ;
                    try
                    {
                        client = new RestClient(Url);
                        var request = new RestRequest(Method.GET);
                        response = client.Execute(request);

                        if (!response.StatusCode.Equals(HttpStatusCode.OK))
                            continue;

                    }
                    catch(Exception e)
                    {
                        if (!response.StatusCode.Equals(HttpStatusCode.OK))
                            continue;
                    }


                    driver.Navigate().GoToUrl(Url);
                    {
                        var isPage2NotFoundErrorPresent = ErrorChecker.CheckFor2PageNotFoundError(driver);
                        var isPageNotFoundErrorPresent = ErrorChecker.CheckForPageNotFoundError(driver);
                        var isDomainErrorsPresent = ErrorChecker.CheckForDomainError(driver);
                        var isNotAcceptrableErrorPresent = ErrorChecker.CheckForNotAcceptableError(driver);
                        var isSuspended = ErrorChecker.SuspendedPage(driver);
                        var isRobotCaptcha = ErrorChecker.RobotCaptchaCheck(driver);
                        var isPageNotLoaded = ErrorChecker.PageNotLoaded(driver);
                        var wordpressSuggestion = ErrorChecker.WordPressSuggestion(driver);
                        if (!wordpressSuggestion.IsNullOrEmpty())
                            wordpressSuggestion.Click();

                        if (isPage2NotFoundErrorPresent || isPageNotFoundErrorPresent || isDomainErrorsPresent || isNotAcceptrableErrorPresent || isNotAcceptrableErrorPresent || isSuspended || isRobotCaptcha || isPageNotLoaded)
                            continue;
                    }
                    
                    //public T GetPageInstance(T page)
                    //{

                    //}
                    WordPressAdminLoginPage LoginPage = new WordPressAdminLoginPage();
                    //var page = Helpers.GetPage<WordPressAdminLoginPage>(driver);
                    PageFactory.InitElements(driver, LoginPage);
                    LoginPage.loginTextField.SendKeys(Login);
                    LoginPage.passwordTextField.SendKeys(Password);
                    LoginPage.loginButton.Click();

                    {
                        var isPage2NotFoundErrorPresent = ErrorChecker.CheckFor2PageNotFoundError(driver);
                        var isPageNotFoundErrorPresent = ErrorChecker.CheckForPageNotFoundError(driver);
                        var isDomainErrorsPresent = ErrorChecker.CheckForDomainError(driver);
                        var isNotAcceptrableErrorPresent = ErrorChecker.CheckForNotAcceptableError(driver);
                        var isSuspended = ErrorChecker.SuspendedPage(driver);
                        var isRobotCaptcha = ErrorChecker.RobotCaptchaCheck(driver);
                        var isPageNotLoaded = ErrorChecker.PageNotLoaded(driver);
                        var wordpressSuggestion = ErrorChecker.WordPressSuggestion(driver);
                        if (!wordpressSuggestion.IsNullOrEmpty())
                            wordpressSuggestion.Click();

                        if (isPage2NotFoundErrorPresent || isPageNotFoundErrorPresent || isDomainErrorsPresent || isNotAcceptrableErrorPresent || isNotAcceptrableErrorPresent || isSuspended || isRobotCaptcha || isPageNotLoaded)
                            continue;
                    }

                    if (!driver.Url.Contains("wp-admin"))
                    {
                        var isIncorrectPassword = ErrorChecker.IncorrectPassword(driver);
                        if (!isIncorrectPassword.IsNullOrEmpty())
                        {
                            if (isIncorrectPassword.Text.Contains("ERROR: Incorrect username or password") || isIncorrectPassword.Text.Contains("ERROR: Invalid username"))
                            {
                                continue;
                            }
                            if (isIncorrectPassword.Displayed)
                            {
                                driver.Navigate().Refresh();
                            }
                        }
                    }
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
                            continue;
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("timed out after 60 seconds."))
                    {
                        SendKeys.SendWait(@"{Esc}");
                        continue;
                    }
                    else
                    {
                        SendKeys.SendWait(@"{Esc}");
                        continue;
                    }
                }
            }
            //DriverExit();
        }
    }
}

