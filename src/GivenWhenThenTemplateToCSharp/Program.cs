using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CommandLine;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;

namespace GivenWhenThenTemplateToCSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = ParseArguments(args);

            ConvertTemplatesToCSharp(options);
        }

        private static Options ParseArguments(string[] args)
        {
            return Parser.Default.ParseArguments<Options>(args)
                .MapResult(
                    opts => opts,
                    errors =>
                    {
                        var errorsList = errors.ToList();

                        if (IsHelpOrVersionRequested(errorsList))
                        {
                            return new Options();
                        }

                        foreach (var error in errorsList)
                        {
                            Console.WriteLine(error);
                        }

                        return new Options();
                    }
                );
        }

        private static bool IsHelpOrVersionRequested(List<Error> errorsList)
        {
            return errorsList.Any(
                x => x.Tag == ErrorType.HelpRequestedError || x.Tag == ErrorType.VersionRequestedError
            );
        }

        private static void ConvertTemplatesToCSharp(Options options)
        {
            foreach (var featureFilePath in options.FeatureFiles)
            {
                ConvertTemplateToCSharp(new FileInfo(featureFilePath), options.Namespace);
            }
        }

        private static void ConvertTemplateToCSharp(FileInfo featureFile, string @namespace)
        {
            var csharpSourceCode = CsharpSourceCode(featureFile, @namespace);
            var csharpSourceCodeFilePath = CsharpSourceCodeFilePath(featureFile);

            File.WriteAllText(csharpSourceCodeFilePath, csharpSourceCode);

            Console.WriteLine("info: C# source code is generated at " + csharpSourceCodeFilePath);
        }

        private static string CsharpSourceCode(FileInfo featureFile, string @namespace)
        {
            var request = new TemplateConversionRequest(
                featureFile,
                @namespace,
                File.ReadAllLines(featureFile.FullName)
            );

            var converter = ConvertTemplateToCSharpHandlerFactory();

            return converter.Handle(request, null);
        }

        private static ConvertTemplateToCSharpHandler ConvertTemplateToCSharpHandlerFactory()
        {
            var context = new ConvertTemplateToCSharpContext();

            return new ConvertTemplateToCSharpHandler(
                new TrimEndFeatureContent(),
                new DetectIndentAdapter(
                    new DetectIndentHandler(new DefaultToTab(), new ParseIndentInformationFromSecondLine()),
                    context
                ),
                new EnrichFileNameAsWrapperTestClass(context),
                new ConvertTemplateToCSharpCore(context)
            );
        }

        private static string CsharpSourceCodeFilePath(FileInfo featureFile)
        {
            return Path.Combine(featureFile.DirectoryName, featureFile.NameWithoutExtension() + "Test.cs");
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        public class Options
        {
            [Option(
                shortName: 'f',
                longName: "features",
                Required = true,
                HelpText = "Feature files to be converted"
            )]
            public IEnumerable<string> FeatureFiles { get; set; } = new string[0];

            [Option(
                shortName: 'n',
                longName: "namespace",
                HelpText = "Set the namespace of the generated tests",
                Default = "TestsNamespace"
            )]
            public string Namespace { get; set; }
        }
    }
}