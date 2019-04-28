using System.IO;
using FluentAssertions;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;
using Xunit;

namespace GivenWhenThenTemplateToCSharp.Tests
{
    public class ConvertTemplateToCSharpTest
    {
        private readonly ConvertTemplateToCSharpHandler _converter;

        protected ConvertTemplateToCSharpTest()
        {
            _converter = new ConvertTemplateToCSharpHandler(
                new TrimEndFeatureContent(),
                new ConvertTemplateToCSharpCore(
                    new DetectIndentHandler(
                        new DefaultToTab(),
                        new ParseIndentInformationFromSecondLine()
                    )
                )
            );
        }

        public class given_a_template_conversion_request_with_drilled_down_scenarios_only : ConvertTemplateToCSharpTest
        {
            private readonly TemplateConversionRequest _request;

            public given_a_template_conversion_request_with_drilled_down_scenarios_only()
            {
                _request =
                    new TemplateConversionRequest(
                        new FileInfo("./data/Scenario1.feature"),
                        "SomeNamespace",
                        new[]
                        {
                            "Given the Maker has started a game with the word silky",
                            "\tWhen the Breaker joins the Maker's game",
                            "\t\tThen the Breaker must guess a word with 5 characters"
                        }
                    );
            }

            [Fact]
            public void then_returns_csharp_source_code_as_file_content()
            {
                // arrange

                // act
                var targetSourceCode = _converter.Handle(_request, null);

                // assert
                targetSourceCode.Should()
                    .Be(
                        "namespace SomeNamespace\n" +
                        "{\n" +
                        "    public class Scenario1\n" +
                        "    {\n" +
                        "        public class Given_the_Maker_has_started_a_game_with_the_word_silky : Scenario1\n" +
                        "        {\n" +
                        "            public class When_the_Breaker_joins_the_Makers_game : Given_the_Maker_has_started_a_game_with_the_word_silky\n" +
                        "            {\n" +
                        "                [Fact]\n" +
                        "                public void Then_the_Breaker_must_guess_a_word_with_5_characters()\n" +
                        "                {\n" +
                        "                    // arrange\n" +
                        "                    \n" +
                        "                    \n" +
                        "                    // act\n" +
                        "                    \n" +
                        "                    // assert\n" +
                        "                }\n" +
                        "            }\n" +
                        "        }\n" +
                        "    }\n" +
                        "}\n"
                    );
            }
        }
    }
}