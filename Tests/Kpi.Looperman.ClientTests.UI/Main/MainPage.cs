using Kpi.Looperman.ClientTests.Model.Platform.Drivers;
using Kpi.Looperman.ClientTests.Model.Platform.Locator;
using Kpi.Looperman.ClientTests.Platform.Page;
using Kpi.Looperman.ClientTests.Platform.WebElements;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.Looperman.ClientTests.UI.Main
{
    public class MainPage : WebPage
    {
        public MainPage(IWebDriver webDriver) 
            : base(webDriver)
        {
        }

        [FindBy(How.XPath, ".//div[@class='nav-account']/ul/li[2]/a")]
        public HtmlButton HeaderButton { get; set; }

        [FindBy(How.XPath, ".//div[@class='nav-sub']/ul/li[@class='']/a")]
        public HtmlButton SiteInfoButton { get; set; }
    }
}
