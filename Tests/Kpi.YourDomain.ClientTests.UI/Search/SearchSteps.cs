using Kpi.YourDomain.ClientTests.Model.Domain.Search;
using Kpi.YourDomain.ClientTests.Model.Platform.Drivers;
using Kpi.YourDomain.ClientTests.Platform.Factory;

namespace Kpi.YourDomain.ClientTests.UI.Search
{
    public class SearchSteps : ISearchSteps
    {
        private readonly IWebDriver _webDriver;

        public SearchSteps(
            IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private MainPage MainPage => PageFactory.Get<MainPage>(_webDriver);

        private SearchElement SearchElement => PageFactory.Get<MainPage>(_webDriver).HeaderSection.SearchElement;

        public void SetValue(string value)
        {
            SearchElement.SetValue(value);
        }

        public void Search()
        {
            SearchElement.Search();
        }

        public void Search(string value)
        {
            SearchElement.Search(value);
        }

        public void Close()
        {
            MainPage.DocSection.CloseButton.GetDisplayed();
            _webDriver.Close();
        }
    }
}
