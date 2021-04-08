using Kpi.YourDomain.ClientTests.Model.Platform.Drivers;
using Kpi.YourDomain.ClientTests.Platform.Configuration.Environment;

namespace Kpi.YourDomain.ClientTests.UI
{
    public class StepsBase
    {
        private readonly IWebDriver _webDriver;
        private readonly IEnvironmentConfiguration _environmentConfiguration;

        protected StepsBase(
            IWebDriver webDriver,
            IEnvironmentConfiguration environmentConfiguration)
        {
            _webDriver = webDriver;
            _environmentConfiguration = environmentConfiguration;
        }

        public void OpenMainView() =>
            _webDriver.Get(_environmentConfiguration.EnvironmentUri);
    }
}
