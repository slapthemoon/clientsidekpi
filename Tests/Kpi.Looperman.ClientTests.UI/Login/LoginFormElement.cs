using Kpi.Looperman.ClientTests.Model.Platform.Locator;
using Kpi.Looperman.ClientTests.Platform.Element;
using Kpi.Looperman.ClientTests.Platform.WebElements;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.Looperman.ClientTests.UI.Login
{
    public class LoginFormElement : HtmlElement
    {
        [FindBy(How.XPath, "//*[@id='user_email']")]
        public HtmlTextBox EmailTextBox { get; set; }

        [FindBy(How.XPath, "//*[@id='upass']")]
        public HtmlTextBox PasswordTextBox { get; set; }

        [FindBy(How.XPath, "//*[@id='submit']")]
        public HtmlButton LoginButton { get; set; }

        [FindBy(How.XPath, "//*[@id='user_disclaimer']")]
        public HtmlCheckbox DisclaimerCheckBox { get; set; }

        [FindBy(How.XPath, "//*[@class='form-error']")]
        public HtmlTextBox ErrorMessage { get; set; }
    }
}
