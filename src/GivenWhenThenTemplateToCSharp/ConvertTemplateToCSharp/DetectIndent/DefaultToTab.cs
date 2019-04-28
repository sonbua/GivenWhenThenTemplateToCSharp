using System;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent
{
    public class DefaultToTab
        : IHandler<string[], string>
    {
        public string Handle(string[] featureContent, Func<string[], string> next)
        {
            return featureContent.Length < 2
                ? "\t"
                : next(featureContent);
        }
    }
}