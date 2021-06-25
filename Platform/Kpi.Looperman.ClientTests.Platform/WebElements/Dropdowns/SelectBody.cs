using System.Linq;
using Kpi.Looperman.ClientTests.Model.Platform.Locator;
using Kpi.Looperman.ClientTests.Platform.Element;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.Looperman.ClientTests.Platform.WebElements.Dropdowns
{
    public class SelectBody : HtmlElement
    {
        public SelectOption[] SelectOptions =>
            FindAll<SelectOption>(new Locator(How.XPath, ".//et-select-body-option"))
                .ToArray();
    }
}
