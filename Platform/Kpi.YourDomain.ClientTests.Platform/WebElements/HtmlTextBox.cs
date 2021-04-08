using Kpi.YourDomain.ClientTests.Model.Platform.WebElements;
using Kpi.YourDomain.ClientTests.Platform.Element;

namespace Kpi.YourDomain.ClientTests.Platform.WebElements
{
    public class HtmlTextBox : HtmlElement, IHtmlTextElement
    {
        public void SetText(string text)
        {
            NativeElement.Clear();
            NativeElement.SendKeys(text);
        }

        public string GetValue() => NativeElement.GetAttribute("value");

        public string GetPlaceholder() => NativeElement.GetAttribute("placeholder");
    }
}
