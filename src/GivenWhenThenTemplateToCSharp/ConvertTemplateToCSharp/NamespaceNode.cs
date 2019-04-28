using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    internal class NamespaceNode : Node
    {
        public NamespaceNode(string name, Node[] children)
            : base(name, Empty, children)
        {
        }

        protected override void AddChild(Node child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (!(child is ClassNode))
            {
                throw new NotSupportedException("Only class is to be added to a namespace");
            }

            Children.Add(child);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var item in ToStringImpl())
            {
                builder.Append(item);
            }

            return builder.ToString();
        }

        private IEnumerable<string> ToStringImpl()
        {
            return NameSpaceSignature()
                .Concat(NamespaceBegin())
                .Concat(NamespaceBody())
                .Concat(NamespaceEnd());
        }

        private IEnumerable<string> NameSpaceSignature()
        {
            yield return "namespace ";
            yield return Name;
            yield return "\n";
        }

        private IEnumerable<string> NamespaceBegin()
        {
            yield return "{\n";
        }

        private IEnumerable<string> NamespaceBody() => Children.Select(x => x.ToString());

        private IEnumerable<string> NamespaceEnd()
        {
            yield return "}\n";
        }
    }
}