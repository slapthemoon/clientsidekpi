using System.Linq;
using TechTalk.SpecFlow;

namespace Kpi.Looperman.ClientTests.Tests.Hooks
{
    [Binding]
    public class StepsTransformation
    {
        [StepArgumentTransformation]
        public string[] TransformToListOfString(string commaSeparatedList) =>
            commaSeparatedList.Split(";")
                .Select(e => e.Trim()).ToArray();

        [StepArgumentTransformation]
        public bool StringToBool(string value) => Bool.Parse(value);
    }
}
