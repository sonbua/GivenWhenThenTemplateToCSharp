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
            var newRequest = new TemplateConversionRequest(
                request.FileInfo,
                request.Namespace,
                NewFeatureContent(request)
            );

            return next(newRequest);
        }

        private string[] NewFeatureContent(TemplateConversionRequest request)
        {
            var wrapperTestClass = request.FileInfo.NameWithoutExtension() + "Test";
            var indentedFeatureContent = request.FeatureContent.Select(line => _context.Indent + line);

            return new[] {wrapperTestClass}.Concat(indentedFeatureContent).ToArray();
        }
    }
}