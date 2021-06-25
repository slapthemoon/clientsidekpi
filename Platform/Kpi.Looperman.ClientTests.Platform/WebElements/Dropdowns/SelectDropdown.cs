using Kpi.Looperman.ClientTests.Model.Platform.Locator;
using Kpi.Looperman.ClientTests.Platform.Element;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.Looperman.ClientTests.Platform.WebElements.Dropdowns
{
    public class SelectDropdown : HtmlElement
    {
        [FindBy(How.XPath, ".//et-deposit-payment-method-dropdown")]
        public MethodDropDown MethodDropDown { get; set; }
    }
}
