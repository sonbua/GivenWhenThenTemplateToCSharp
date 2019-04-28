namespace GivenWhenThenTemplateToCSharp
{
    internal static class StringExtensions
    {
        public static string Remove(this string source, string toBeRemoved)
        {
            return source.Replace(toBeRemoved, string.Empty);
        }
    }
}