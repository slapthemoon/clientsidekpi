using OpenQA.Selenium;

namespace Kpi.YourDomain.ClientTests.Model.Platform.Element
{
    public interface IHtmlElement : ISearchable, INative
    {
        bool Exists { get; }

        bool Enabled { get; }

        SearchStrategy SearchStrategy { get; set; }

        INative Parent { get; set; }

        void SetNativeElement(IWebElement nativeElement);

        void Click();

        string GetText();

        bool GetDisplayed();

        IWebElement GetNativeElement();

        string GetAttribute(string property);

        void SendKeys(string value);
    }
}
