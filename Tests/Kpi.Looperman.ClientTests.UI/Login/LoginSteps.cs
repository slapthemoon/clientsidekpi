using Kpi.Looperman.ClientTests.Model.Domain.Login;
using Kpi.Looperman.ClientTests.Model.Platform.Drivers;
using Kpi.Looperman.ClientTests.Platform.Configuration.Environment;
using Kpi.Looperman.ClientTests.Platform.Factory;
using Kpi.Looperman.ClientTests.UI.Main;

namespace Kpi.Looperman.ClientTests.UI.Login
{
    public class LoginSteps : StepsBase, ILoginSteps
    {
        public LoginSteps(
            IWebDriver webDriver,
            IEnvironmentConfiguration environmentConfiguration)
            : base(webDriver, environmentConfiguration)
        {
        }

        private LoginFormElement LoginForm => 
            PageFactory.Get<LoginPage>(WebDriver).LoginForm;

        private MainPage MainPage => 
            PageFactory.Get<MainPage>(WebDriver);

        public void SetEmail(string email)
        {
            LoginForm.EmailTextBox.SetText(email);
        }

        public void SetPassword(string password)
        {
            LoginForm.PasswordTextBox.SetText(password);
        }

        public void CheckDisclaimer()
        {
            LoginForm.DisclaimerCheckBox.Check();
        }

        public void Login()
        {
            LoginForm.LoginButton.Click();
        }

        public void OpenLoginPage()
        {
            OpenMainView();
            MainPage.HeaderButton.Click();
        }

        public string GetErrorMessage()
        {
            return LoginForm.ErrorMessage.GetText().Trim();
        }
    }
}
