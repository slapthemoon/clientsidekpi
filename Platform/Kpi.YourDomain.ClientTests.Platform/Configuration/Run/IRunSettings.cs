using Kpi.YourDomain.ClientTests.Model.Domain.Run;

namespace Kpi.YourDomain.ClientTests.Platform.Configuration.Run
{
    public interface IRunSettings
    {
        SeleniumGrid SeleniumGrid { get; set; }

        RunType RunType { get; set; }
    }
}
