using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Model.Platform.WebElements.AutoComplete;
using Kpi.YourDomain.ClientTests.Platform.Element;
using Kpi.YourDomain.ClientTests.Platform.Waiter;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.Platform.WebElements.AutoComplete
{
    public class HtmlAutoCompleteTextField : HtmlElement, IHtmlAutoCompleteTextField
    {
        [FindBy(How.XPath, ".//input")]
        private HtmlTextBox Input { get; set; }

        [FindBy(How.XPath, ".//label")]
        private HtmlLabel HtmlLabel { get; set; }

        private HtmlAutocompleteDropdown HtmlAutocompleteDropdown =>
            Find<HtmlAutocompleteDropdown>(
                new Locator(How.XPath, ".//ancestor::body//div[@id='autocomplete-results']"));

        public void SetText(string value) => Input.SetText(value);

        public string GetLabel() => HtmlLabel.GetText();

        public string GetPlaceHolder() => Input.GetAttribute("placeholder");

        public string GetValue() => Input.GetText();

        public string[] GetDropdownValues() => HtmlAutocompleteDropdown.GetValues();

        public void SetValue(string value)
        {
            SetText(value);
            WaitFor.Condition(() => HtmlAutocompleteDropdown.GetDisplayed(), "The 'Autocomplete' dropdown was not displayed.");
            HtmlAutocompleteDropdown.Select(value);
        }
    }
}
