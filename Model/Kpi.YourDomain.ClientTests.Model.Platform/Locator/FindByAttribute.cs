using System;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.YourDomain.ClientTests.Model.Platform.Locator
{
    public class FindByAttribute : Attribute
    {
        public FindByAttribute(How how, string @using)
        {
            How = how;
            Using = @using;
        }

        public How How { get; }

        public string Using { get; }
    }
}
