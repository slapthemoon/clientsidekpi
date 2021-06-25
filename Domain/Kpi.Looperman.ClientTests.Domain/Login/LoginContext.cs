using Kpi.Looperman.ClientTests.Model.Domain.Login;

namespace Kpi.Looperman.ClientTests.Domain.Login
{
    public class LoginContext : ILoginContext
    {
        private readonly ILoginSteps _loginSteps;

        public LoginContext(ILoginSteps loginSteps)
        {
            _loginSteps = loginSteps;
        }

        public void OpenAndLogin(UserInformation user)
        {
            _loginSteps.OpenLoginPage();
            Login(user);
        }

        private void Login(UserInformation user)
        {
            _loginSteps.SetEmail(user.Email);
            _loginSteps.SetPassword(user.Password);
            _loginSteps.CheckDisclaimer();
            _loginSteps.Login();
        }
    }
}
