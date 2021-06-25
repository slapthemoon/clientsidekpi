using Kpi.Looperman.ClientTests.Model.Platform.Locator;
using Kpi.Looperman.ClientTests.Platform.Element;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.Looperman.ClientTests.Platform.WebElements.Dropdowns
{
    public class SelectOption : HtmlElement
    {
        [FindBy(How.XPath, ".//span")]
        public HtmlLabel Name { get; set; }
    }
}
