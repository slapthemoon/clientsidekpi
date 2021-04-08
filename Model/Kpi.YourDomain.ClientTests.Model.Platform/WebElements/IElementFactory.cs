using Kpi.YourDomain.ClientTests.Model.Platform.Element;

namespace Kpi.YourDomain.ClientTests.Model.Platform.WebElements
{
    public interface IElementFactory
    {
        void InitProperties(INative htmlElement);
    }
}
