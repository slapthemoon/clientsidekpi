using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Model.Platform.WebElements;
using Kpi.YourDomain.ClientTests.Platform.Element;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.Platform.WebElements
{
    public class HtmlCheckbox : HtmlElement, IHtmlCheckbox
    {
        [FindBy(How.XPath, ".//input[@type='checkbox']")]
        private HtmlElement Input { get; set; }

        public void Check()
        {
            if (!IsChecked())
            {
                Click();
            }
        }

        public void Uncheck()
        {
            if (IsChecked())
            {
                Click();
            }
        }

        public void SetValue(bool value)
        {
            if (IsChecked() != value)
            {
                Click();
            }
        }

        public bool IsChecked()
        {
            if (!Input.Exists)
            {
                return HasClass("checked");
            }

            return Input.GetAttribute("checked") != null;
        }

        private new void Click()
        {
            if (Input.Exists)
            {
                Input.Click();
            }
            else
            {
                base.Click();
            }
        }
    }
}
