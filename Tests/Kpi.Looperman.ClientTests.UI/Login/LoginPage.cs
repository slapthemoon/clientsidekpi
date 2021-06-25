using Kpi.Looperman.ClientTests.Model.Platform.Drivers;
using Kpi.Looperman.ClientTests.Model.Platform.Locator;
using Kpi.Looperman.ClientTests.Platform.Page;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.Looperman.ClientTests.UI.Login
{
    public class LoginPage : WebPage
    {
        public LoginPage(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        [FindBy(How.XPath, "//*[@id='login']")]
        public LoginFormElement LoginForm { get; set; }
    }
}
