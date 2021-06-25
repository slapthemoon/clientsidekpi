using Kpi.Looperman.ClientTests.Model.Platform.Drivers;
using Kpi.Looperman.ClientTests.Model.Platform.Locator;
using Kpi.Looperman.ClientTests.Platform.Page;
using Kpi.Looperman.ClientTests.Platform.WebElements;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.Looperman.ClientTests.UI.Info
{
    public class InfoPage : WebPage
    {
        public InfoPage(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        [FindBy(How.XPath, "//*[@class='section-title']")]
        public HtmlLabel Title { get; set; }
    }
}
