using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kpi.YourDomain.ClientTests.Model.Platform.Element;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Factory;
using Kpi.YourDomain.ClientTests.Platform.Retry;
using Kpi.YourDomain.ClientTests.Platform.Waiter;
using OpenQA.Selenium;

namespace Kpi.YourDomain.ClientTests.Platform.Element
{
    public class HtmlElement : IHtmlElement
    {
        private const int RetryCount = 2;

        private IWebElement _nativeElement;

        public virtual bool Exists
        {
            get
            {
                try
                {
                    // Poke element.
                    // ReSharper disable once UnusedVariable
                    var text = ReInitNativeElement().Text;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public virtual bool Enabled =>
            Do.Retry(() => GetDisplayed() && NativeElement.Enabled, RetryCount);

        public SearchStrategy SearchStrategy { get; set; }

        public INative Parent { get; set; }

        protected IWebElement NativeElement
        {
            get
            {
                var timeout = Stopwatch.StartNew();
                Exception innerException = null;
                while (timeout.Elapsed <= TimeSpan.FromSeconds(30))
                {
                    try
                    {
                        return ReInitNativeElement();
                    }
                    catch (NotFoundException ex)
                    {
                        innerException = ex;
                    }
                    catch (StaleElementReferenceException ex)
                    {
                        innerException = ex;
                    }
                    catch (InvalidElementStateException ex)
                    {
                        innerException = ex;
                    }
                }

                throw new ElementNotFoundException($"Can not find element {SearchStrategy.Locator.Using}", innerException);
            }
        }

        public void SendKeys(string value) => NativeElement.SendKeys(value);

        public TElement Find<TElement>(Locator locator)
            where TElement : IHtmlElement, new()
        {
            return ElementFactory.Create<TElement>(this, locator);
        }

        public IEnumerable<TElement> FindAll<TElement>(Locator locator) 
            where TElement : IHtmlElement, new()
        {
            var elementsCollection = CollectionFactory.Create<TElement>(this, locator);
            foreach (var element in elementsCollection)
            {
                yield return element;
            }
        }

        IWebElement INative.FindElement(Locator locator, int index) =>
            NativeElement.FindElement(locator.ToSeleniumLocator());

        IReadOnlyCollection<IWebElement> INative.FindElements(Locator locator) =>
            NativeElement.FindElements(locator.ToSeleniumLocator());

        public string GetAttribute(string attribute) =>
            Do.Retry(() => NativeElement.GetAttribute(attribute));

        public virtual bool GetDisplayed()
        {
            return Do.Retry(() => Exists && NativeElement.Displayed, RetryCount);
        }

        void IHtmlElement.SetNativeElement(IWebElement nativeElement)
        {
            _nativeElement = nativeElement;
        }

        IWebElement IHtmlElement.GetNativeElement() => NativeElement;

        public virtual void Click()
        {
            Wait.For(() =>
            {
                try
                {
                    NativeElement.Click();
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (InvalidOperationException e)
                {
                    if (e.Message.Contains("is not clickable at point"))
                    {
                        return false;
                    }

                    throw;
                }
            });
        }

        public virtual string GetText()
        {
            return Do.Retry(
                () =>
            {
                var textValue = NativeElement.Text;
                return string.IsNullOrEmpty(textValue) ? GetAttribute("textContent")?.Trim() : textValue;
            }, RetryCount);
        }

        public override string ToString() =>
            $"{GetType().Name}. {SearchStrategy.Locator}";

        public bool HasClass(string className) =>
            NativeElement.GetAttribute("class").Contains(className);

        public bool HasProperty(string property) =>
            NativeElement.GetProperty(property) != null;

        private IWebElement ReInitNativeElement()
        {
            if (_nativeElement == null || !_nativeElement.Exists())
            {
                _nativeElement = Parent.FindElement(SearchStrategy.Locator, SearchStrategy.Index);
            }

            return _nativeElement;
        }
    }
}
