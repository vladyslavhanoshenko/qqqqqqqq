using OpenQA.Selenium;
using System;

namespace TestingFramework.Helpers
{
    class ErrorsChecker
    {
        public bool CheckForNotAcceptableError(IWebDriver driver)
        {
            try
            {
                var notAcceptable = driver.FindElement(By.XPath("html/body/h1[contains(text(),'Not Acceptable!')]"));
                if (notAcceptable.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public bool CheckForPageNotFoundError(IWebDriver driver)
        {
            try
            {
                var pageNotFoundError = driver.FindElement(By.XPath("//span[@class='status-code'][contains(text(),'404')]"));
                if (pageNotFoundError.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public bool CheckForDomainError(IWebDriver driver)
        {


            try
            {
                var pageNotFoundError = driver.FindElement(By.XPath("//p[contains(text(),'This domain has expired.')]"));
                if (pageNotFoundError.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool CheckFor2PageNotFoundError(IWebDriver driver)
        {
            try
            {
                var pageNotFoundError = driver.FindElement(By.XPath("//h1[contains(text(),'404 Not Found')]"));
                if (pageNotFoundError.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public bool RobotCaptchaCheck(IWebDriver driver)
        {
            if (driver.Url.Contains("robotcaptcha"))
            {
                return true;
            }
            return false;
        }

        public bool SuspendedPage(IWebDriver driver)
        {
            if (driver.Url.Contains("suspendedpage"))
            {
                return true;
            }
            return false;
        }
        public bool PageNotLoaded(IWebDriver driver)
        {
            try
            {
                var pageNotLoaded = driver.FindElement(By.XPath("//div[@id='main-message']"));

                if (pageNotLoaded.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public IWebElement WordPressSuggestion(IWebDriver driver)
        {
            try
            {
                var wordPressSuggestion = driver.FindElement(By.XPath("//a[@class='jetpack-sso-toggle wpcom']"));
                if (wordPressSuggestion.Displayed)
                {
                    return wordPressSuggestion;
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public bool TechnicalDifficulties(IWebDriver driver)
        {
            try
            {
                var technicalDifficulties = driver.FindElement(By.XPath("//body[@id='error-page']/p[contains(text(), 'The site is experiencing technical difficulties. Please check your site admin email inbox for instructions.')]"));
                if (technicalDifficulties.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;

        }

        public bool Forbidden(IWebDriver driver)
        {
            try
            {
                var forbidden = driver.FindElement(By.XPath("html/body/h1[contains(text(),'Forbidden')]"));
                if (forbidden.Displayed)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public IWebElement IncorrectPassword(IWebDriver driver)
        {
            try
            {
                var loginError = driver.FindElement(By.XPath("//div[@id='login_error']"));
                return loginError;

            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
