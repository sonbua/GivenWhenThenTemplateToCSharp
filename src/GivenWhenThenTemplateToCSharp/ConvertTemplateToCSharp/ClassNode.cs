using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    internal class ClassNode : Node
    {
        public ClassNode(string name, Node parent, Node[] children)
            : base(name, parent, children)
        {
        }

        protected override void AddChild(Node child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (child is NamespaceNode)
            {
                throw new ArgumentException("Namespace cannot be added to a class");
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
            return ClassSignature()
                .Concat(ClassBegin())
                .Concat(ClassBody())
                .Concat(ClassEnd());
        }

        private IEnumerable<string> ClassSignature()
        {
            yield return Indent();
            yield return $"public class {Name.ToClassName()}";

            if (Parent is NamespaceNode)
            {
                yield return "\n";
                yield break;
            }

            yield return " : ";
            yield return Parent.Name.ToClassName();
            yield return "\n";
        }

        private IEnumerable<string> ClassBegin() => new[] {Indent(), "{\n"};

        private IEnumerable<string> ClassBody() => Children.Select(child => child.ToString());

        private IEnumerable<string> ClassEnd() => new[] {Indent(), "}\n"};
    }
}