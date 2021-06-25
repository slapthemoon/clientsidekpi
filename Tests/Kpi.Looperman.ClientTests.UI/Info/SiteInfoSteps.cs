using Kpi.Looperman.ClientTests.Model.Domain.SiteInfo;
using Kpi.Looperman.ClientTests.Model.Platform.Drivers;
using Kpi.Looperman.ClientTests.Platform.Configuration.Environment;
using Kpi.Looperman.ClientTests.Platform.Factory;
using Kpi.Looperman.ClientTests.UI.Info;

namespace Kpi.Looperman.ClientTests.UI.Main
{
    public class SiteInfoSteps : StepsBase, ISiteInfoSteps
    {
        public SiteInfoSteps(
            IWebDriver webDriver,
            IEnvironmentConfiguration environmentConfiguration)
            : base(webDriver, environmentConfiguration)
        {
        }

        private MainPage MainPage =>
            PageFactory.Get<MainPage>(WebDriver);

        private InfoPage InfoPage =>
            PageFactory.Get<InfoPage>(WebDriver);

        public void OpenInfoPage()
        {
            MainPage.SiteInfoButton.Click();
        }

        public string GetPageTittle()
        {
            return InfoPage.Title.GetText().Trim();
        }
    }
}
