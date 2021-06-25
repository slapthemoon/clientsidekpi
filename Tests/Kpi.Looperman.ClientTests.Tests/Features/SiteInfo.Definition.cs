using FluentAssertions;
using Kpi.Looperman.ClientTests.Model.Domain.SiteInfo;
using TechTalk.SpecFlow;

namespace Kpi.Looperman.ClientTests.Tests.Features
{
    [Binding, Scope(Feature = "Site Info")]
    public class SiteInfo
    {
        private readonly ISiteInfoSteps _siteInfoSteps;

        public SiteInfo(
            ISiteInfoSteps siteInfoSteps)
        {
            _siteInfoSteps = siteInfoSteps;
        }

        [Given(@"I have opened main page")]
        public void GivenIHaveOpenedMainPage()
        {
            _siteInfoSteps.OpenMainView();
        }

        [When(@"I click What is Looperman \? button")]
        public void WhenIClickWhatIsLoopermanButton()
        {
            _siteInfoSteps.OpenInfoPage();
        }

        [Then(@"I see '(.*)' as title of the page")]
        public void ThenISeeAsTitleOfThePage(string title)
        {
            _siteInfoSteps.GetPageTittle().Should().BeEquivalentTo(title);
        }
    }
}
