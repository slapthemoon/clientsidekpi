namespace Kpi.Looperman.ClientTests.Model.Domain.Login
{
    public interface ILoginSteps
    {
        void SetEmail (string email);

        void SetPassword (string password);

        void Login ();

        string GetErrorMessage ();

        void CheckDisclaimer ();

        void OpenMainView ();

        void OpenLoginPage ();
    }
}
