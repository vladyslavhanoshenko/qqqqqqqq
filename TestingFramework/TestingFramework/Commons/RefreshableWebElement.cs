

using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace TestingFramework.Commons
{
    public class RefreshableWebElement : IWebElement
    {
        private IWebDriver driver;
        private IWebElement element;

        public RefreshableWebElement(IWebDriver driver, IWebElement element)
        {
            this.driver = driver;
            this.element = element;
        }
        private String getLocatorFromWebElement(IWebElement element)
        {
            var test = element.ToString();
            //return element.ToString().Split("->".ToCharArray)[1].replaceFirst("(?s)(.*)\\]", "$1" + "");

            return null;
        }

        public string TagName => throw new NotImplementedException();

        public string Text => throw new NotImplementedException();

        public bool Enabled => throw new NotImplementedException();

        public bool Selected => throw new NotImplementedException();

        public Point Location => throw new NotImplementedException();

        public Size Size => throw new NotImplementedException();

        public bool Displayed => throw new NotImplementedException();

        Point IWebElement.Location => throw new NotImplementedException();

        Size IWebElement.Size => throw new NotImplementedException();

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Click()
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string attributeName)
        {
            throw new NotImplementedException();
        }

        public string GetCssValue(string propertyName)
        {
            throw new NotImplementedException();
        }

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void SendKeys(string text)
        {
            throw new NotImplementedException();
        }

        public void Submit()
        {
            throw new NotImplementedException();
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            throw new NotImplementedException();
        }
    }
}
