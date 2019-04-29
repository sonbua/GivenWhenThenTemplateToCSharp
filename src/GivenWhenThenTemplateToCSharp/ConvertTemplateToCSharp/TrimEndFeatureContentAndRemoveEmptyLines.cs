using System;
using System.Linq;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class TrimEndFeatureContentAndRemoveEmptyLines
        : IHandler<ConvertTemplateToCSharpRequest, string>
    {
        public string Handle(ConvertTemplateToCSharpRequest request, Func<ConvertTemplateToCSharpRequest, string> next)
        {
            var featureContent =
                request.FeatureContent
                    .Select(x => x.TrimEnd())
                    .Where(line => !string.IsNullOrEmpty(line))
                    .ToArray();

            return next(new ConvertTemplateToCSharpRequest(request.FileInfo, request.Namespace, featureContent));
        }
    }
}