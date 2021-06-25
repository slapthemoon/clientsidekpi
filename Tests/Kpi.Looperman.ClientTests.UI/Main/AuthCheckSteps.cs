using Kpi.Looperman.ClientTests.Model.Domain.AuthInfo;
using Kpi.Looperman.ClientTests.Model.Platform.Drivers;
using Kpi.Looperman.ClientTests.Platform.Configuration.Environment;
using Kpi.Looperman.ClientTests.Platform.Factory;

namespace Kpi.Looperman.ClientTests.UI.Main
{
    public class AuthCheckSteps : StepsBase, IAuthCheckSteps
    {
        public AuthCheckSteps(
            IWebDriver webDriver,
            IEnvironmentConfiguration environmentConfiguration)
            : base(webDriver, environmentConfiguration)
        {
        }

        private MainPage MainPage =>
            PageFactory.Get<MainPage>(WebDriver);

        public string GetButtonText()
        {
            return MainPage.HeaderButton.GetText().Trim();
        }
    }
}
