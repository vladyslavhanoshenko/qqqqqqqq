using OpenQA.Selenium.Support.UI;
using System;

namespace TestingFramework.Commons
{
    public class BaseSteps
    {
        protected WebDriverWait wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, 10));
    }
}
