namespace GivenWhenThenTemplateToCSharp
{
    internal static class StringExtensions
    {
        public static string ToClassName(this string name)
        {
            return name.Cleanup();
        }

        public static string ToMethodName(this string name)
        {
            return name.Cleanup();
        }

        private static string Cleanup(this string name)
        {
            return name.Replace(' ', '_')
                .Remove("\"")
                .Remove("'");
        }

        private static string Remove(this string source, string toBeRemoved)
        {
            return source.Replace(toBeRemoved, string.Empty);
        }
    }
}