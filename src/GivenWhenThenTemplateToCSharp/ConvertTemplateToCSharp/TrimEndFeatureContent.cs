using System;
using System.Linq;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class TrimEndFeatureContent
        : IHandler<TemplateConversionRequest, string>
    {
        public string Handle(TemplateConversionRequest request, Func<TemplateConversionRequest, string> next)
        {
            var featureContent =
                request.FeatureContent
                    .Select(x => x.TrimEnd())
                    .Where(line => !string.IsNullOrEmpty(line))
                    .ToArray();

            return next(new TemplateConversionRequest(request.FileInfo, request.Namespace, featureContent));
        }
    }
}