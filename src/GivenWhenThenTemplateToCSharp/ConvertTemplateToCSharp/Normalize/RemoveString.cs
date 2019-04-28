using System;
using System.Collections.Generic;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize
{
    public class RemoveString
        : IHandler<string, string>
    {
        private readonly List<string> _toBeRemoved;

        public RemoveString()
        {
            _toBeRemoved = new List<string> {"'", "\"", "!", "."};
        }

        public string Handle(string name, Func<string, string> next)
        {
            foreach (var s in _toBeRemoved)
            {
                name = name.Remove(s);
            }

            return next(name);
        }
    }
}