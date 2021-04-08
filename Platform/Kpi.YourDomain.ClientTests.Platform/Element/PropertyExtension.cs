using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kpi.YourDomain.ClientTests.Model.Platform.Element;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;

namespace Kpi.YourDomain.ClientTests.Platform.Element
{
    internal static class PropertyExtension
    {
        internal static bool IsHtmlElement(this PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsHtmlElement();
        }

        internal static bool HasLocatorAttribute(this PropertyInfo propertyInfo) =>
            propertyInfo.CustomAttributes
                .Any(attribute =>
                    attribute.AttributeType == typeof(FindByAttribute));

        internal static Locator GetLocatorAttribute(this PropertyInfo propertyInfo)
        {
            var findByAttribute =
                (FindByAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(FindByAttribute));
            return findByAttribute.ToLocator();
        }

        internal static bool HasUrlAttribute(this Type pageType)
        {
            var attributes = pageType.CustomAttributes;
            return attributes.Any(attribute => attribute.AttributeType == typeof(UrlAttribute));
        }

        internal static UrlAttribute GetUrlAttribute(this Type pageType)
        {
            var urlAttribute = (UrlAttribute)Attribute.GetCustomAttribute(pageType, typeof(UrlAttribute));
            return urlAttribute;
        }

        internal static bool IsHtmlElementCollection(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.PropertyType.IsGenericType)
            {
                return false;
            }

            var interfaces = propertyInfo.PropertyType.GetInterfaces();
            if (interfaces.All(i => i != typeof(IEnumerable<>)))
            {
                return false;
            }

            return propertyInfo.PropertyType
                       .GenericTypeArguments.Length == 1
                   && propertyInfo.PropertyType
                       .GenericTypeArguments.Single()
                       .IsHtmlElement();
        }

        private static bool IsHtmlElement(this Type type) =>
            type.GetInterfaces().Any(i => i == typeof(IHtmlElement));
    }
}
