namespace Kpi.Looperman.ClientTests.Platform.String
{
    public static class StringExtensions
    {
        public static string WithArguments(this string initialString, params object[] args) =>
            string.Format(initialString, args);
    }
}
