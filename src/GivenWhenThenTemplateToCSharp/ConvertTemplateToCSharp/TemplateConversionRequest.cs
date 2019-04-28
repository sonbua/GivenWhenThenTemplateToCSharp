using System.IO;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class TemplateConversionRequest
    {
        public TemplateConversionRequest(FileInfo fileInfo, string @namespace, string[] featureContent)
        {
            FileInfo = fileInfo;
            Namespace = @namespace;
            FeatureContent = featureContent;
        }

        public FileInfo FileInfo { get; }
        public string Namespace { get; }
        public string[] FeatureContent { get; }
    }
}