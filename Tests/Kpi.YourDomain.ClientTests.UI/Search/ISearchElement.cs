namespace Kpi.YourDomain.ClientTests.UI.Search
{
    public interface ISearchElement
    {
        void SetValue (string value);

        void Search ();

        void Search (string value);
    }
}
