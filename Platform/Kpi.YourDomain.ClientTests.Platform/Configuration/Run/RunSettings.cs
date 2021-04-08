using Kpi.YourDomain.ClientTests.Model.Domain.Run;

namespace Kpi.YourDomain.ClientTests.Platform.Configuration.Run
{
    public class RunSettings : IRunSettings
    {
        public SeleniumGrid SeleniumGrid { get; set; }

        public RunType RunType { get; set; }
    }
}
