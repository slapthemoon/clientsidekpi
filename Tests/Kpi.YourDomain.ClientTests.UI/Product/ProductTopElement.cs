using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Element;
using Kpi.YourDomain.ClientTests.Platform.WebElements;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.UI.Product
{
    public class ProductTopElement : HtmlElement
    {
        [FindBy(How.XPath, ".//h1")]
        public HtmlLabel ProductHeaderLabel { get; set; }
    }
}
