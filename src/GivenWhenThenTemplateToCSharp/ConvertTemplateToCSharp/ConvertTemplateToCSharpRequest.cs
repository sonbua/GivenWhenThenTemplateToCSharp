using System.IO;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp
{
    public class ConvertTemplateToCSharpRequest
    {
        public ConvertTemplateToCSharpRequest(FileInfo fileInfo, string @namespace, string[] featureContent)
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