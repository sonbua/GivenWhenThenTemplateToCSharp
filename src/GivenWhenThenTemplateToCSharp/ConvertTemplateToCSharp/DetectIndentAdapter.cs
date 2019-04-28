using System;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class DetectIndentAdapter
        : IHandler<TemplateConversionRequest, string>
    {
        private readonly DetectIndentHandler _detectIndentHandler;
        private readonly ConvertTemplateToCSharpContext _context;

        public DetectIndentAdapter(DetectIndentHandler detectIndentHandler, ConvertTemplateToCSharpContext context)
        {
            _detectIndentHandler = detectIndentHandler;
            _context = context;
        }

        public string Handle(TemplateConversionRequest request, Func<TemplateConversionRequest, string> next)
        {
            _context.Indent = _detectIndentHandler.Handle(request.FeatureContent, null);

            return next(request);
        }
    }
}