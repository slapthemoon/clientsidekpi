using Kpi.Looperman.ClientTests.Model.Platform.WebElements;
using Kpi.Looperman.ClientTests.Platform.Element;

namespace Kpi.Looperman.ClientTests.Platform.WebElements
{
    public class HtmlLink : HtmlElement, IHtmlLink
    {
        public string GetLink() => GetAttribute("href");

        public new void Click() => base.Click();
    }
}
