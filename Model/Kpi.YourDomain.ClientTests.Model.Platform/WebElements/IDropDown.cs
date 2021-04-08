namespace Kpi.YourDomain.ClientTests.Model.Platform.WebElements
{
    public interface IDropDown
    {
        string[] GetOptions();

        void Choose(string option);
    }
}
