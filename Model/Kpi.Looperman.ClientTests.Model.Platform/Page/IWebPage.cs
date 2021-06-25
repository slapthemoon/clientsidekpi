using Kpi.Looperman.ClientTests.Model.Platform.Element;

namespace Kpi.Looperman.ClientTests.Model.Platform.Page
{
    public interface IWebPage : ISearchable, INative
    {
        string Address { get; set; }

        string Title { get; set; }

        void Open();

        void Refresh();
    }
}
