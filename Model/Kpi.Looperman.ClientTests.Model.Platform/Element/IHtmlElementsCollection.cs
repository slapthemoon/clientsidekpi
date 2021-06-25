using System.Collections.Generic;

namespace Kpi.Looperman.ClientTests.Model.Platform.Element
{
    public interface IHtmlElementsCollection<out THtmlElement> : IEnumerable<THtmlElement> 
        where THtmlElement : IHtmlElement
    {
    }
}
