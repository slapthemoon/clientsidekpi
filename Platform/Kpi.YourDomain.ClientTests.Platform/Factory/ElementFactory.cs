using System;
using System.Reflection;
using Kpi.YourDomain.ClientTests.Model.Platform.Element;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Element;

namespace Kpi.YourDomain.ClientTests.Platform.Factory
{
    internal static class ElementFactory
    {
        public static TElement Create<TElement>(INative parent, Locator locator)
            where TElement : IHtmlElement =>
            (TElement)Create(typeof(TElement), parent, locator);

        public static TElement Create<TElement>(INative parent, Locator locator, int index)
            where TElement : IHtmlElement, new() =>
            (TElement)Create(typeof(TElement), parent, locator, index);

        internal static void InitProperties(INative htmlElement)
        {
            var properties = htmlElement.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                if (property.HasLocatorAttribute() && property.IsHtmlElement())
                {
                    var element = Create(property.PropertyType, htmlElement, property.GetLocatorAttribute());
                    property.SetValue(htmlElement, element);
                }

                if (property.HasLocatorAttribute() && property.IsHtmlElementCollection())
                {
                    var collection = CollectionFactory.Create(property.PropertyType, htmlElement,
                        property.GetLocatorAttribute());
                    property.SetValue(htmlElement, collection);
                }

                if (property.HasLocatorAttribute() && !(property.IsHtmlElement() || property.IsHtmlElementCollection()))
                {
                    throw new InvalidOperationException(
                        $"Property {property.Name} has FindBy attribute but is not collection or element");
                }
            }
        }

        private static IHtmlElement Create(Type elementType, INative parent, Locator locator, int index = 0)
        {
            var htmlElement = (IHtmlElement)Activator.CreateInstance(elementType);
            htmlElement.Parent = parent;
            htmlElement.SearchStrategy = new SearchStrategy(locator, index);
            InitProperties(htmlElement);
            return htmlElement;
        }
    }
}
