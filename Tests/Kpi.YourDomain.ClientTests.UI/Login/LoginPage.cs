using System;
using System.Linq;
using Kpi.YourDomain.ClientTests.Model.Platform.Drivers;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Page;
using Kpi.YourDomain.ClientTests.Platform.WebElements;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.UI.Login
{
    public class LoginPage : WebPage
    {
        public LoginPage(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        [FindBy(How.XPath, ".//input[@id='auth_email']")]
        public HtmlTextBox EmailTextBox { get; set; }

        [FindBy(How.XPath, ".//input[@id='auth_pass']")]
        public HtmlTextBox PasswordTextBox { get; set; }

        public HtmlButton[] LoginButton =>
            FindAll<HtmlButton>(new Locator(How.XPath, string.Empty)).ToArray();
    }
}
