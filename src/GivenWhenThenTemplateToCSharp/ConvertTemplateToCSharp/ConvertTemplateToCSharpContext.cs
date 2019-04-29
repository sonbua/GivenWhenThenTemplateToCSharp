using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpContext
        : IChainContext<ConvertTemplateToCSharpHandler, ConvertTemplateToCSharpRequest, string>
    {
        public string Indent { get; set; }
    }
}