using System;
using System.Linq;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class EnrichFileNameAsWrapperTestClass
        : IHandler<ConvertTemplateToCSharpRequest, string>
    {
        private readonly ConvertTemplateToCSharpContext _context;

        public EnrichFileNameAsWrapperTestClass(ConvertTemplateToCSharpContext context)
        {
            _context = context;
        }

        public string Handle(ConvertTemplateToCSharpRequest request, Func<ConvertTemplateToCSharpRequest, string> next)
        {
            var newRequest = new ConvertTemplateToCSharpRequest(
                request.FileInfo,
                request.Namespace,
                NewFeatureContent(request)
            );

            return next(newRequest);
        }

        private string[] NewFeatureContent(ConvertTemplateToCSharpRequest request)
        {
            var featureName = request.FileInfo.NameWithoutExtension();
            var wrapperTestClass = featureName + "Test";
            var indentedFeatureContent = request.FeatureContent.Select(line => _context.Indent + line);

            return new[] {wrapperTestClass}.Concat(indentedFeatureContent).ToArray();
        }
    }
}