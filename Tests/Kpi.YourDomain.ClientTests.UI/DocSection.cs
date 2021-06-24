using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Element;
using Kpi.YourDomain.ClientTests.Platform.WebElements;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.UI
{
    public class DocSection : HtmlElement
    {
        [FindBy(How.XPath, "")]
        public HtmlButton CloseButton { get; set; }
    }
}