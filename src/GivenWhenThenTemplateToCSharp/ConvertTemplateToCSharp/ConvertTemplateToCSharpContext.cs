using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpContext
        : IChainContext<ConvertTemplateToCSharpHandler, TemplateConversionRequest, string>
    {
        public string Indent { get; set; }
    }
}