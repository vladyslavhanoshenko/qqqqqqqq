using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace TestingFramework.Pages.Sms_activate
{
    public class NumbersPanel : MainPage
    {
        public IWebElement AnyServiceNumber { get
            {
                return driver.FindElement(By.XPath("(//span[contains(text(), 'Любой другой')])[2]"));
            } }

        public NumbersPanel(IWebDriver driver) : base(driver)
        {

        }

        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> GetAllTableNumbers() => driver.FindElements(By.XPath("//form[@id='leftForm']/table/tbody"));
    }
}
