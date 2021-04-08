namespace Kpi.YourDomain.ClientTests.Model.Platform.WebElements.Mat
{
    public interface IMatSelect
    {
        void Open();

        void Select(string option);

        string[] GetOptions();
    }
}
