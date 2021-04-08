using Kpi.YourDomain.ClientTests.Model.Platform.WebElements;
using Kpi.YourDomain.ClientTests.Platform.Element;

namespace Kpi.YourDomain.ClientTests.Platform.WebElements
{
    public class HtmlLink : HtmlElement, IHtmlLink
    {
        public string GetLink() => GetAttribute("href");

        public new void Click() => base.Click();
    }
}
