using System;
using System.Linq;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class EnrichFileNameAsWrapperTestClass
        : IHandler<TemplateConversionRequest, string>
    {
        private readonly ConvertTemplateToCSharpContext _context;

        public EnrichFileNameAsWrapperTestClass(ConvertTemplateToCSharpContext context)
        {
            _context = context;
        }

        public string Handle(TemplateConversionRequest request, Func<TemplateConversionRequest, string> next)
        {
            var featureContent =
                new[] {request.FileInfo.NameWithoutExtension() + "Test"}
                    .Concat(request.FeatureContent.Select(line => _context.Indent + line))
                    .ToArray();

            return next(new TemplateConversionRequest(request.FileInfo, request.Namespace, featureContent));
        }
    }
}