using System;

namespace Kpi.YourDomain.ClientTests.Platform.Element
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string exception)
            : base(exception)
        {
        }

        public ElementNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
