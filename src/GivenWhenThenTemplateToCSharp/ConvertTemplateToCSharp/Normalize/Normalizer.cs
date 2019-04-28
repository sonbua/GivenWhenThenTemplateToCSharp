using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize
{
    public class Normalizer
        : Handler<string, string>
    {
        public Normalizer(
            ReplaceWithUnderscore replaceWithUnderscore,
            RemoveString removeString,
            ReturnAsIs returnAsIs)
        {
            AddHandler(replaceWithUnderscore);
            AddHandler(removeString);
            AddHandler(returnAsIs);
        }
    }
}