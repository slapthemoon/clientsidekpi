namespace Kpi.YourDomain.ClientTests.Model.Platform.Element
{
    public class SearchStrategy
    {
        public SearchStrategy(Locator.Locator locator)
        {
            Locator = locator;
        }

        public SearchStrategy(Locator.Locator locator, int index)
        {
            Locator = locator;
            Index = index;
        }

        public SearchStrategy()
        {
        }

        public Locator.Locator Locator { get; }

        public int Index { get; }
    }
}
