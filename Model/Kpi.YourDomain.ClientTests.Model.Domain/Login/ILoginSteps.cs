namespace Kpi.YourDomain.ClientTests.Model.Domain.Login
{
    public interface ILoginSteps
    {
        void SetEmail (string email);

        void SetPassword (string password);

        void Login ();

        void OpenLoginPage ();

        void OpenMainView ();
    }
}
