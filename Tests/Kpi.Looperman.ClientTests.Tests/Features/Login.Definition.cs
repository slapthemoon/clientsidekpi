using FluentAssertions;
using Kpi.Looperman.ClientTests.Model.Domain.AuthInfo;
using Kpi.Looperman.ClientTests.Model.Domain.Login;
using Kpi.Looperman.ClientTests.TestsData.Storage;
using TechTalk.SpecFlow;

namespace Kpi.Looperman.ClientTests.Tests.Features
{
    [Binding, Scope(Feature = "Login")]
    public class LoginDefinition
    {
        private readonly ILoginContext _loginContext;
        private readonly ILoginSteps _loginSteps;
        private readonly IAuthCheckSteps _authCheckSteps;
        private UserInformation _userInformation;

        public LoginDefinition(
            ILoginContext loginContext,
            IAuthCheckSteps authCheckSteps,
            ILoginSteps loginSteps)
        {
            _loginContext = loginContext;
            _loginSteps = loginSteps;
            _authCheckSteps = authCheckSteps;
        }

        [Given(@"I have (.*) user")]
        public void GivenIHaveExistingUserUser(string entityName)
        {
            _userInformation = UsersStorage.Users[entityName];
        }

        [When(@"I login to application")]
        public void WhenILoginToApplication()
        {
            _loginContext.OpenAndLogin(_userInformation);
        }

        [Then(@"I see '(.*)' button in header")]
        public void ThenISeeButtonInHeader(string text)
        {
            _authCheckSteps.GetButtonText().Should()
                .BeEquivalentTo(text);
        }

        [Then(@"I see '(.*)' message")]
        public void ThenISeeMessageInForm(string message)
        {
            _loginSteps.GetErrorMessage().Should().BeEquivalentTo(message);
        }
    }
}
