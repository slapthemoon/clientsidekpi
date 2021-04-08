using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Element;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.Platform.WebElements.Dropdowns
{
    public class SelectDropdown : HtmlElement
    {
        [FindBy(How.XPath, ".//et-deposit-payment-method-dropdown")]
        public MethodDropDown MethodDropDown { get; set; }
    }
}
