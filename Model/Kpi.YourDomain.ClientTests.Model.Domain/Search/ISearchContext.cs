namespace Kpi.YourDomain.ClientTests.Model.Domain.Search
{
    public interface ISearchContext
    {
        void Search (string value);

        void SearchAndClose (string value);
    }
}
