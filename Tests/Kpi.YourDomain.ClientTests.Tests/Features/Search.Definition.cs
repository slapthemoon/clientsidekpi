using FluentAssertions;
using Kpi.YourDomain.ClientTests.Model.Domain.Login;
using Kpi.YourDomain.ClientTests.Model.Domain.Poduct;
using Kpi.YourDomain.ClientTests.Model.Domain.Search;
using TechTalk.SpecFlow;

namespace Kpi.YourDomain.ClientTests.Tests.Features
{
    [Binding, Scope(Feature = "Search")]
    public class SearchDefinition
    {
        private readonly ILoginSteps _loginSteps;
        private readonly ISearchContext _searchContext;
        private readonly IProductTopSteps _productTopSteps;

        public SearchDefinition (
            ILoginSteps loginSteps,
            ISearchContext searchContext,
            IProductTopSteps productTopSteps)
        {
            _loginSteps = loginSteps;
            _searchContext = searchContext;
            _productTopSteps = productTopSteps;
        }

        [Given(@"I open main view")]
        public void GivenIOpenMainView ()
        {
            _loginSteps.OpenMainView();
        }

        [When(@"I search '(.*)' value")]
        public void WhenISearchValue (string value)
        {
            _searchContext.Search(value);
        }

        [Then(@"I see '(.*)' result")]
        public void ThenISeeResults (string expectedValue)
        {
            var actualValue = _productTopSteps.GetTitle();
            actualValue.Should().Be(expectedValue);
        }
    }
}
