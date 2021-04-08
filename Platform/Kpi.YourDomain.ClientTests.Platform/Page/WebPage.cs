using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Kpi.YourDomain.ClientTests.Model.Platform.Element;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Model.Platform.Page;
using Kpi.YourDomain.ClientTests.Platform.Driver;
using Kpi.YourDomain.ClientTests.Platform.Element;
using Kpi.YourDomain.ClientTests.Platform.Factory;
using OpenQA.Selenium;
using IJavaScriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using IWebDriver = Kpi.YourDomain.ClientTests.Model.Platform.Drivers.IWebDriver;

namespace Kpi.YourDomain.ClientTests.Platform.Page
{
    public abstract class WebPage : IWebPage
    {
        private readonly IWebDriver _webDriver;
        private readonly OpenQA.Selenium.IWebDriver _nativeDriver;

        protected WebPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _nativeDriver = ((WebDriver)_webDriver).GetNativeDriver();
        }

        string IWebPage.Address { get; set; }

        string IWebPage.Title { get; set; }

        public TElement Find<TElement>(Locator locator) 
            where TElement : IHtmlElement, new() =>
            ElementFactory.Create<TElement>(this, locator);

        public IEnumerable<TElement> FindAll<TElement>(Locator locator) 
            where TElement : IHtmlElement, new()
        {
            IHtmlElementsCollection<TElement> elementsCollection = new HtmlElementsCollection<TElement>(this, locator);
            foreach (var element in elementsCollection)
            {
                yield return element;
            }
        }

        IWebElement INative.FindElement(Locator locator, int index) =>
            ((WebDriver)_webDriver).GetElement(locator, index);

        IReadOnlyCollection<IWebElement> INative.FindElements(Locator locator) =>
            ((WebDriver)_webDriver).GetElements(locator);

        public void Open() =>
            _webDriver.Get(((IWebPage)this).Address);

        public virtual void Refresh() =>
            _webDriver.Refresh();

        public override string ToString() =>
            ((IWebPage)this).Address;

        public bool WaitForPageReady(TimeSpan timeout)
        {
            var result = true;
            var stop = false;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!stop)
            {
                long requests = -1;
                string readystate = null;
                try
                {
                    // ReSharper disable once PossibleNullReferenceException
                    requests = (long)((IJavaScriptExecutor)_nativeDriver)?.ExecuteScript("return jQuery.active");
                }
                catch (UnhandledAlertException)
                {
                    stop = true;
                }
                catch (NoSuchWindowException)
                {
                    stop = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("jQuery is not defined"))
                    {
                    }
                }

                try
                {
                    // ReSharper disable once PossibleNullReferenceException
                    readystate = (string)((IJavaScriptExecutor)_nativeDriver).ExecuteScript("return document.readyState");
                }
                catch (UnhandledAlertException)
                {
                    stop = true;
                }
                catch (NoSuchWindowException)
                {
                    stop = true;
                }
                catch (Exception)
                {
                    // ignored
                }

                stop = stop || ("complete".Equals(readystate) && requests <= 0);
                if (!stop)
                {
                    Thread.Sleep(100);
                }

                if (stopwatch.Elapsed >= timeout)
                {
                    stop = true;
                    result = false;
                }
            }

            return result;
        }
    }
}
