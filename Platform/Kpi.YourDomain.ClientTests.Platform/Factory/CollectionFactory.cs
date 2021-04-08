using System;
using Kpi.YourDomain.ClientTests.Model.Platform.Element;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Element;

namespace Kpi.YourDomain.ClientTests.Platform.Factory
{
    internal static class CollectionFactory
    {
        internal static IHtmlElementsCollection<THtmlElement> Create<THtmlElement>(
            INative parent,
            Locator locator)
            where THtmlElement : IHtmlElement, new() =>
                new HtmlElementsCollection<THtmlElement>(parent, locator);

        internal static object Create(Type type, INative parent, Locator locator)
        {
            var concreteType = typeof(HtmlElementsCollection<>)
                .MakeGenericType(type.GetGenericArguments());
            return Activator.CreateInstance(concreteType, parent, locator);
        }
    }
}
