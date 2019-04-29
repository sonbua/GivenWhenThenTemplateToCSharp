using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandLine;
using GivenWhenThenTemplateToCSharp.ConvertMultipleTemplatesToCSharp;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize;

namespace GivenWhenThenTemplateToCSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = ParseArguments(args);

            var handler = ConvertMultipleTemplatesToCSharpHandlerFactory();

            handler.Handle(
                new ConvertMultipleTemplatesToCSharpRequest(options.FeatureFiles.ToArray(), options.Namespace),
                null
            );
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

        private static ConvertMultipleTemplatesToCSharpHandler ConvertMultipleTemplatesToCSharpHandlerFactory()
        {
            var context = new ConvertTemplateToCSharpContext();

            return new ConvertMultipleTemplatesToCSharpHandler(
                new ConvertTemplateToCSharpHandler(
                    new TrimEndFeatureContentAndRemoveEmptyLines(),
                    new DetectIndentAdapter(
                        new DetectIndentHandler(new DefaultToTab(), new ParseIndentInformationFromSecondLine()),
                        context
                    ),
                    new EnrichFileNameAsWrapperTestClass(context),
                    new ConvertTemplateToCSharpCore(
                        context,
                        new Normalizer(
                            new ReplaceWithUnderscore(),
                            new RemoveString(),
                            new ReturnAsIs()
                        )
                    )
                )
            );
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