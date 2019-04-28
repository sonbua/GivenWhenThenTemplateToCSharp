using System;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpCore
        : IHandler<TemplateConversionRequest, string>
    {
        private readonly ConvertTemplateToCSharpContext _context;

        public ConvertTemplateToCSharpCore(ConvertTemplateToCSharpContext context)
        {
            _context = context;
        }

        public string Handle(TemplateConversionRequest request, Func<TemplateConversionRequest, string> next)
        {
            var rootNode = new NamespaceNode(request.Namespace, new Node[0]);

            var previousNode = (Node) rootNode;

            foreach (var line in request.FeatureContent)
            {
                previousNode = Node.Parse(line, previousNode, _context.Indent);
            }

            return rootNode.ToString();
        }
    }
}