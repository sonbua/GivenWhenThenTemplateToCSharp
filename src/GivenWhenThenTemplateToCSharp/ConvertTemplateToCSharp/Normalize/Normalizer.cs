using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize
{
    public class Normalizer
        : Handler<string, string>
    {
        public Normalizer(
            ReplaceWithUnderscore replaceWithUnderscore,
            RemoveSpecialCharacters removeSpecialCharacters,
            ReturnAsIs returnAsIs)
        {
            AddHandler(replaceWithUnderscore);
            AddHandler(removeSpecialCharacters);
            AddHandler(returnAsIs);
        }
    }
}