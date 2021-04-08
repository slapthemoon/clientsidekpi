using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Element;
using Kpi.YourDomain.ClientTests.Platform.WebElements;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.UI.Search
{
    public class SearchElement : HtmlElement, ISearchElement
    {
        [FindBy(How.XPath, ".//input[@name='search']")]
        private HtmlTextBox SearchHtmlTextBox { get; set; }

        [FindBy(How.XPath, ".//button[contains(@class, 'search-form__submit')]")]
        private HtmlButton SearchButton { get; set; }

        public void SetValue(string value)
        {
            SearchHtmlTextBox.SetText(value);
        }

        public void Search()
        {
            SearchButton.Click();
        }

        public void Search(string value)
        {
            SetValue(value);
            Search();
        }
    }
}
