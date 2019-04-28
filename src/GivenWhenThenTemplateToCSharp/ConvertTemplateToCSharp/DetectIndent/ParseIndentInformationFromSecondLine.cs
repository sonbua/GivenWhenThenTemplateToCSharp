using System;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent
{
    public class ParseIndentInformationFromSecondLine
        : IHandler<string[], string>
    {
        public string Handle(string[] featureContent, Func<string[], string> next)
        {
            var lineWithIndent = featureContent[1];

            return lineWithIndent.Substring(0, lineWithIndent.Length - lineWithIndent.Trim().Length);
        }
    }
}