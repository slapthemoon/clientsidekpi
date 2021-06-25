using Kpi.Looperman.ClientTests.Model.Platform.WebElements;
using Kpi.Looperman.ClientTests.Platform.Element;

namespace Kpi.Looperman.ClientTests.Platform.WebElements
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
