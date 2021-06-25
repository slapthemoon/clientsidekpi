using System;

namespace Kpi.Looperman.ClientTests.Platform.Element
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
