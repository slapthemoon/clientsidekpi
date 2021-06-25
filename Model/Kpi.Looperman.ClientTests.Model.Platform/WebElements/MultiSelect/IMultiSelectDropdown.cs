namespace Kpi.Looperman.ClientTests.Model.Platform.WebElements.MultiSelect
{
    public interface IMultiSelectDropdown
    {
        void SelectItems(params string[] values);

        string[] GetSelected();

        void OpenDropdown();

        void CloseDropdown();

        string[] GetAll();
    }
}
