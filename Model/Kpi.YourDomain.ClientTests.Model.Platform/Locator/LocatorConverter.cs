using System;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.Model.Platform.Locator
{
    public static class LocatorConverter
    {
        public static OpenQA.Selenium.By ToSeleniumLocator(this Locator locator)
        {
            return locator.How switch
            {
                How.Id => OpenQA.Selenium.By.Id(locator.Using),
                How.CssSelector => OpenQA.Selenium.By.CssSelector(locator.Using),
                How.TagName => OpenQA.Selenium.By.TagName(locator.Using),
                How.XPath => OpenQA.Selenium.By.XPath(locator.Using),
                How.ClassName => OpenQA.Selenium.By.ClassName(locator.Using),
                How.LinkText => OpenQA.Selenium.By.LinkText(locator.Using),
                How.Name => OpenQA.Selenium.By.Name(locator.Using),
                How.PartialLinkText => OpenQA.Selenium.By.PartialLinkText(locator.Using),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static Locator ToLocator(this FindByAttribute attribute) =>
            new Locator(attribute.How, attribute.Using);
    }
}
