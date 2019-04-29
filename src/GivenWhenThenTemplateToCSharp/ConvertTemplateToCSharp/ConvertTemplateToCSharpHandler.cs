using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpHandler : Handler<ConvertTemplateToCSharpRequest, string>
    {
        public ConvertTemplateToCSharpHandler(
            TrimEndFeatureContentAndRemoveEmptyLines trimEndFeatureContentAndRemoveEmptyLines,
            DetectIndentAdapter detectIndentAdapter,
            EnrichFileNameAsWrapperTestClass enrichFileNameAsWrapperTestClass,
            ConvertTemplateToCSharpCore convertTemplateToCSharpCore)
        {
            AddHandler(trimEndFeatureContentAndRemoveEmptyLines);
            AddHandler(detectIndentAdapter);
            AddHandler(enrichFileNameAsWrapperTestClass);
            AddHandler(convertTemplateToCSharpCore);
        }
    }
}