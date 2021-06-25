using Kpi.Looperman.ClientTests.Model.Domain.Run;

namespace Kpi.Looperman.ClientTests.Platform.Configuration.Run
{
    public class RunSettings : IRunSettings
    {
        public SeleniumGrid SeleniumGrid { get; set; }

        public RunType RunType { get; set; }
    }
}
