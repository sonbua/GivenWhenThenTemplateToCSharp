using System;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpCore
        : IHandler<ConvertTemplateToCSharpRequest, string>
    {
        private readonly ConvertTemplateToCSharpContext _context;
        private readonly Normalizer _normalizer;

        public ConvertTemplateToCSharpCore(ConvertTemplateToCSharpContext context, Normalizer normalizer)
        {
            _context = context;
            _normalizer = normalizer;
        }

        public string Handle(ConvertTemplateToCSharpRequest request, Func<ConvertTemplateToCSharpRequest, string> next)
        {
            var rootNode = new NamespaceNode(request.Namespace, new Node[0]);

            var previousNode = (Node) rootNode;

            foreach (var line in request.FeatureContent)
            {
                previousNode = Node.Parse(line, _normalizer, previousNode, _context.Indent);
            }

            return rootNode.ToString();
        }
    }
}