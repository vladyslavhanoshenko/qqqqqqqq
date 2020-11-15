using OpenQA.Selenium.Support.UI;
using TestingFramework.Commons;
using TestingFramework.Pages;

namespace TestingFramework.Steps
{
    public class DominosProfileSteps : BaseSteps
    {
        protected DominosProfilePage profilePage = new DominosProfilePage();

        public void WaitUntilProfileSaveButtonDisplayed()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(profilePage.ProfileSaveButton));
        }
    }
}
