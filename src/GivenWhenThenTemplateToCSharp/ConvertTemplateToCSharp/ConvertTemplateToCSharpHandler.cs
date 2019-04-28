using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpHandler : Handler<TemplateConversionRequest, string>
    {
        public ConvertTemplateToCSharpHandler(
            TrimEndFeatureContent trimEndFeatureContent,
            DetectIndentAdapter detectIndentAdapter,
            EnrichFileNameAsWrapperTestClass enrichFileNameAsWrapperTestClass,
            ConvertTemplateToCSharpCore convertTemplateToCSharpCore)
        {
            AddHandler(trimEndFeatureContent);
            AddHandler(detectIndentAdapter);
            AddHandler(enrichFileNameAsWrapperTestClass);
            AddHandler(convertTemplateToCSharpCore);
        }
    }
}