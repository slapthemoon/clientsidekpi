using Kpi.YourDomain.ClientTests.Model.Platform.Element;

namespace Kpi.YourDomain.ClientTests.Model.Platform.WebElements
{
    public interface IHtmlTextElement : IHtmlElement
    {
        void SetText(string text);

        string GetValue();

        string GetPlaceholder();
    }
}
