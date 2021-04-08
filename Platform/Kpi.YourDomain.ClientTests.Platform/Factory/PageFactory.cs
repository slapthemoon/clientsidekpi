using System;
using Kpi.YourDomain.ClientTests.Model.Platform.Drivers;
using Kpi.YourDomain.ClientTests.Model.Platform.Page;
using Kpi.YourDomain.ClientTests.Platform.Element;

namespace Kpi.YourDomain.ClientTests.Platform.Factory
{
    public static class PageFactory
    {
        public static TPage Get<TPage>(IWebDriver driver) 
            where TPage : IWebPage
        {
            IWebPage page = (TPage)Activator.CreateInstance(typeof(TPage), driver);
            InitPage(page);
            return (TPage)page;
        }

        private static void InitPage(IWebPage page)
        {
            if (page.GetType().HasUrlAttribute())
            {
                page.Address = page.GetType().GetUrlAttribute().Url;
            }

            ElementFactory.InitProperties(page);
        }
    }
}
