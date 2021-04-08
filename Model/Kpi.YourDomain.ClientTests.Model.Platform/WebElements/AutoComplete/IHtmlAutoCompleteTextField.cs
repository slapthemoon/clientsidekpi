namespace Kpi.YourDomain.ClientTests.Model.Platform.WebElements.AutoComplete
{
    public interface IHtmlAutoCompleteTextField
    {
        string GetLabel();

        string GetPlaceHolder();

        string GetValue();

        void SetValue(string value);

        void SetText(string value);

        string[] GetDropdownValues();
    }
}
