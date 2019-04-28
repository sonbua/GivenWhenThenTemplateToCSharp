using System;
using System.Collections.Generic;
using System.Linq;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    internal abstract class Node
    {
        private Node()
        {
        }

        protected Node(string name, Node parent, Node[] children)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name should not be null or empty");
            }

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            if (!(parent is EmptyNode))
            {
                parent.AddChild(this);
            }

            Name = name;
            Parent = parent;
            Children = children.ToList();
        }

        protected static readonly Node Empty = new EmptyNode();

        public string Name { get; }

        protected Node Parent { get; }
        protected List<Node> Children { get; }

        private int Level
        {
            get
            {
                if (Parent == Empty)
                {
                    return 0;
                }

                return Parent.Level + 1;
            }
        }

        public static Node Parse(string line, Normalizer normalizer, Node previous, string indent)
        {
            var name = normalizer.Handle(line.Trim(), null);
            var indentLevel = IndentLevel(line, indent);
            var parent = previous.TraverseUp(indentLevel - 1);

            return name.ToLowerInvariant().StartsWith("then")
                ? (Node) new MethodNode(name, parent)
                : new ClassNode(name, parent, new Node[0]);
        }

        private static int IndentLevel(string line, string indent)
        {
            var indentLevel = 1;

            while (line.StartsWith(indent))
            {
                line = line.Substring(indent.Length);
                indentLevel += 1;
            }

            return indentLevel;
        }

        protected abstract void AddChild(Node child);

        protected string Indent() => string.Join(string.Empty, Enumerable.Range(0, Level).Select(_ => "    "));

        protected string IndentBody() => string.Join(string.Empty, Enumerable.Range(0, Level + 1).Select(_ => "    "));

        private Node TraverseUp(int parentIndentLevel)
        {
            var parent = this;

            while (parent.Level > parentIndentLevel)
            {
                parent = parent.Parent;
            }

            return parent;
        }

        private class EmptyNode : Node
        {
            protected override void AddChild(Node child)
            {
                throw new NotSupportedException();
            }
        }
    }
}