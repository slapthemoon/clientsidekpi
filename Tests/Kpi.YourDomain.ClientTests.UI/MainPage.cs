using Kpi.YourDomain.ClientTests.Model.Platform.Drivers;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Page;
using Kpi.YourDomain.ClientTests.Platform.WebElements;
using Kpi.YourDomain.ClientTests.UI.Product;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.UI
{
    public class MainPage : WebPage
    {
        public MainPage(IWebDriver webDriver) 
            : base(webDriver)
        {
        }

        [FindBy(How.XPath, ".//rz-user//button")]
        public HtmlButton OpenLoginButton { get; set; }

        [FindBy(How.XPath, ".//header")]
        public HeaderSection HeaderSection { get; set; }

        [FindBy(How.XPath, ".//rz-product-top")]
        public ProductTopElement ProductTopElement { get; set; }

        [FindBy(How.XPath, ".//rz-product-top")]
        public DocSection DocSection { get; set; }
    }
}
