using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    internal class MethodNode : Node
    {
        public MethodNode(string name, Node parent)
            : base(name, parent, children: new Node[0])
        {
        }

        protected override void AddChild(Node child)
        {
            throw new NotSupportedException($"Cannot add child '{child.Name}' to a method '{Name}'");
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
            return MethodSignature()
                .Concat(MethodBegin())
                .Concat(MethodBody())
                .Concat(MethodEnd());
        }

        private IEnumerable<string> MethodSignature()
        {
            return new[] {Indent(), "[Fact]\n", Indent(), $"public void {Name.ToMethodName()}()\n"};
        }

        private IEnumerable<string> MethodBegin()
        {
            return new[] {Indent(), "{\n"};
        }

        private IEnumerable<string> MethodBody()
        {
            return new[]
            {
                IndentBody(),
                "// arrange\n",
                IndentBody(),
                "\n",
                IndentBody(),
                "\n",
                IndentBody(),
                "// act\n",
                IndentBody(),
                "\n",
                IndentBody(),
                "// assert\n"
            };
        }

        private IEnumerable<string> MethodEnd()
        {
            return new[] {Indent(), "}\n"};
        }
    }
}