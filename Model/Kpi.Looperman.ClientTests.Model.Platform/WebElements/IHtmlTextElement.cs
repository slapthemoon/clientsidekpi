using Kpi.Looperman.ClientTests.Model.Platform.Element;

namespace Kpi.Looperman.ClientTests.Model.Platform.WebElements
{
    public interface IHtmlTextElement : IHtmlElement
    {
        void SetText(string text);

        string GetValue();

        string GetPlaceholder();
    }
}
