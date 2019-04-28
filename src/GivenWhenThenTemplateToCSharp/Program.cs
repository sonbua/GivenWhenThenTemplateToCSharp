using System;
using System.IO;
using System.Linq;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;

namespace GivenWhenThenTemplateToCSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var request = Request(args.First());
            var converter = new ConvertTemplateToCSharpHandler(
                new TrimEndFeatureContent(),
                new ConvertTemplateToCSharpCore(
                    new DetectIndentHandler(new DefaultToTab(), new ParseIndentInformationFromSecondLine())
                )
            );
            var csharpSourceCode = converter.Handle(request, null);

            Console.WriteLine(csharpSourceCode);

            Console.ReadLine();
        }

        private static TemplateConversionRequest Request(string feature)
        {
            var featureFileInfo = new FileInfo(feature);

            return new TemplateConversionRequest(featureFileInfo, "SomeNamespace", File.ReadAllLines(feature));
        }
    }
}