using System;
using System.IO;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp;
using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertMultipleTemplatesToCSharp
{
    public class ConvertMultipleTemplatesToCSharpHandler
        : IHandler<ConvertMultipleTemplatesToCSharpRequest, Nothing>
    {
        private readonly ConvertTemplateToCSharpHandler _convertTemplateToCSharpHandler;

        public ConvertMultipleTemplatesToCSharpHandler(ConvertTemplateToCSharpHandler convertTemplateToCSharpHandler)
        {
            _convertTemplateToCSharpHandler = convertTemplateToCSharpHandler;
        }

        public Nothing Handle(
            ConvertMultipleTemplatesToCSharpRequest request,
            Func<ConvertMultipleTemplatesToCSharpRequest, Nothing> next)
        {
            foreach (var featureFilePath in request.FeatureFiles)
            {
                var featureFile = new FileInfo(featureFilePath);
                var @namespace = request.Namespace;

                ConvertTemplateToCSharp(featureFile, @namespace);
            }

            return new Nothing();
        }

        private void ConvertTemplateToCSharp(FileInfo featureFile, string @namespace)
        {
            var convertTemplateToCSharpRequest = new ConvertTemplateToCSharpRequest(
                featureFile,
                @namespace,
                File.ReadAllLines(featureFile.FullName)
            );

            var csharpSourceCode = _convertTemplateToCSharpHandler.Handle(
                convertTemplateToCSharpRequest,
                null
            );

            var csharpSourceCodeFilePath = Path.Combine(
                featureFile.DirectoryName,
                featureFile.NameWithoutExtension() + "Test.cs"
            );

            File.WriteAllText(csharpSourceCodeFilePath, csharpSourceCode);

            Console.WriteLine("info: C# source code is generated at " + csharpSourceCodeFilePath);
        }
    }
}