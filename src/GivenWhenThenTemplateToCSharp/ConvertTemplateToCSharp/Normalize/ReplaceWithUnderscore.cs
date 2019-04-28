using System;
using System.Collections.Generic;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize
{
    public class ReplaceWithUnderscore
        : IHandler<string, string>
    {
        private readonly List<char> _toBeReplaced;

        public ReplaceWithUnderscore()
        {
            _toBeReplaced = new List<char> {' ', '-'};
        }

        public string Handle(string name, Func<string, string> next)
        {
            foreach (var c in _toBeReplaced)
            {
                name = name.Replace(c, '_');
            }

            return next(name);
        }
    }
}