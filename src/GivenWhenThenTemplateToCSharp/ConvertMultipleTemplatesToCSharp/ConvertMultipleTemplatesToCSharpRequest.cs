namespace GivenWhenThenTemplateToCSharp.ConvertMultipleTemplatesToCSharp
{
    public class ConvertMultipleTemplatesToCSharpRequest
    {
        public ConvertMultipleTemplatesToCSharpRequest(string[] featureFiles, string @namespace)
        {
            FeatureFiles = featureFiles;
            Namespace = @namespace;
        }

        public string[] FeatureFiles { get; }
        public string Namespace { get; }
    }
}