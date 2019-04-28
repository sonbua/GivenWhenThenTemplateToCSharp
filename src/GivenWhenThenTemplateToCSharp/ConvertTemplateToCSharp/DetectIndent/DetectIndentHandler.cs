using GivenWhenThenTemplateToCSharp.ResponsibilityChain;

namespace GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent
{
    public class DetectIndentHandler : Handler<string[], string>
    {
        public DetectIndentHandler(DefaultToTab defaultToTab, ParseIndentInformationFromSecondLine parseIndentInformationFromSecondLine)
        {
            AddHandler(defaultToTab);
            AddHandler(parseIndentInformationFromSecondLine);
        }
    }
}