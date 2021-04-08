namespace Kpi.YourDomain.ClientTests.Model.Platform.WebElements.MultiSelect
{
    public interface IMultiSelectItem
    {
        bool IsSelected();

        void Check();

        void UnCheck();

        void SetValue(string name, bool value);

        void Select(string value);

        string GetName();
    }
}
