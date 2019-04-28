using System;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize
{
    public class ReturnAsIs
        : IHandler<string, string>
    {
        public string Handle(string name, Func<string, string> next)
        {
            return name;
        }
    }
}