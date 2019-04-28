using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpHandler : Handler<TemplateConversionRequest, string>
    {
        public ConvertTemplateToCSharpHandler(
            TrimEndFeatureContent trimEndFeatureContent,
            ConvertTemplateToCSharpCore convertTemplateToCSharpCore)
        {
            AddHandler(trimEndFeatureContent);
            AddHandler(convertTemplateToCSharpCore);
        }
    }
}