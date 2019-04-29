using System;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class DetectIndentAdapter
        : IHandler<ConvertTemplateToCSharpRequest, string>
    {
        private readonly DetectIndentHandler _detectIndentHandler;
        private readonly ConvertTemplateToCSharpContext _context;

        public DetectIndentAdapter(DetectIndentHandler detectIndentHandler, ConvertTemplateToCSharpContext context)
        {
            _detectIndentHandler = detectIndentHandler;
            _context = context;
        }

        public string Handle(ConvertTemplateToCSharpRequest request, Func<ConvertTemplateToCSharpRequest, string> next)
        {
            _context.Indent = _detectIndentHandler.Handle(request.FeatureContent, null);

            return next(request);
        }
    }
}