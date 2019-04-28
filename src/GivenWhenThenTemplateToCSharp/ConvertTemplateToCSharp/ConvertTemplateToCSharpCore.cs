using System;
using System.Linq;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpCore
        : IHandler<TemplateConversionRequest, string>
    {
        private readonly DetectIndentHandler _detectIndentHandler;

        public ConvertTemplateToCSharpCore(DetectIndentHandler detectIndentHandler)
        {
            _detectIndentHandler = detectIndentHandler;
        }

        public string Handle(TemplateConversionRequest request, Func<TemplateConversionRequest, string> next)
        {
            var indent = _detectIndentHandler.Handle(request.FeatureContent, null);

            var featureContent =
                new[] {request.FileInfo.NameWithoutExtension()}
                    .Concat(request.FeatureContent.Select(line => indent + line))
                    .ToArray();

            var rootNode = new NamespaceNode(request.Namespace, new Node[0]);

            var previousNode = (Node) rootNode;

            foreach (var line in featureContent)
            {
                previousNode = Node.Parse(line, previousNode, indent);
            }

            return rootNode.ToString();
        }
    }
}