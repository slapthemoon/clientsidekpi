using Kpi.Looperman.ClientTests.Model.Domain.Run;

namespace Kpi.Looperman.ClientTests.Platform.Configuration.Run
{
    public interface IRunSettings
    {
        SeleniumGrid SeleniumGrid { get; set; }

        RunType RunType { get; set; }
    }
}
