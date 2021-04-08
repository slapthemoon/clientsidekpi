using System;
using OpenQA.Selenium;

namespace Kpi.YourDomain.ClientTests.Platform.Element
{
    internal static class WebElementExtensions
    {
        public static bool Exists(this IWebElement element)
        {
            try
            {
                // Poke element.
                // ReSharper disable once UnusedVariable
                var text = element.Text;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
