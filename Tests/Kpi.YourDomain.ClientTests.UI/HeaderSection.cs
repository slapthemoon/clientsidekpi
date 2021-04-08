using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Element;
using Kpi.YourDomain.ClientTests.UI.Search;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.UI
{
    public class HeaderSection : HtmlElement
    {
        [FindBy(How.XPath, ".//div[@class='header-search js-app-search-suggest']")]
        public SearchElement SearchElement { get; set; }
    }
}
