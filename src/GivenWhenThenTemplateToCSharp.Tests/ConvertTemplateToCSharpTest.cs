using System.IO;
using FluentAssertions;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.DetectIndent;
using GivenWhenThenTemplateToCSharp.ConvertTemplateToCSharp.Normalize;
using Xunit;

namespace GivenWhenThenTemplateToCSharp.Tests
{
    public class ConvertTemplateToCSharpTest
    {
        private readonly ConvertTemplateToCSharpHandler _converter;

        protected ConvertTemplateToCSharpTest()
        {
            var context = new ConvertTemplateToCSharpContext();

            _converter = new ConvertTemplateToCSharpHandler(
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
                        new RemoveSpecialCharacters(),
                        new ReturnAsIs()
                    )
                )
            );
        }

        public class given_a_template_conversion_request_with_drilled_down_scenarios_only : ConvertTemplateToCSharpTest
        {
            private readonly ConvertTemplateToCSharpRequest _request;

            public given_a_template_conversion_request_with_drilled_down_scenarios_only()
            {
                _request =
                    new ConvertTemplateToCSharpRequest(
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
                        "    public class Scenario1Test\n" +
                        "    {\n" +
                        "        public class Given_the_Maker_has_started_a_game_with_the_word_silky : Scenario1Test\n" +
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

        public class given_multiple_given_when_then_in_a_feature_with_blank_line : ConvertTemplateToCSharpTest
        {
            private readonly ConvertTemplateToCSharpRequest _request;

            public given_multiple_given_when_then_in_a_feature_with_blank_line()
            {
                _request = new ConvertTemplateToCSharpRequest(
                    new FileInfo("MultipleSiteSupport.feature"),
                    "Tests",
                    new[]
                    {
                        "Given I am logged in as Dr. Bill",
                        "  When I try to post to \"Expensive Therapy\"",
                        "    Then I should see \"Your article was published.\"",
                        "  When I try to post to \"Greg's anti-tax rants\"",
                        "    Then I should see \"Hey! That's not your blog!\"",
                        "",
                        "Given I am logged in as Greg",
                        "  When I try to post to \"Expensive Therapy\"",
                        "    Then I should see \"Your article was published.\""
                    }
                );
            }

            [Fact]
            public void then_converts_to_csharp_source_code()
            {
                // arrange

                // act
                var csharpSourceCode = _converter.Handle(_request, null);

                // assert
                csharpSourceCode.Should()
                    .Be(
                        "namespace Tests\n" +
                        "{\n" +
                        "    public class MultipleSiteSupportTest\n" +
                        "    {\n" +
                        "        public class Given_I_am_logged_in_as_Dr_Bill : MultipleSiteSupportTest\n" +
                        "        {\n" +
                        "            public class When_I_try_to_post_to_Expensive_Therapy : Given_I_am_logged_in_as_Dr_Bill\n" +
                        "            {\n" +
                        "                [Fact]\n" +
                        "                public void Then_I_should_see_Your_article_was_published()\n" +
                        "                {\n" +
                        "                    // arrange\n" +
                        "                    \n" +
                        "                    \n" +
                        "                    // act\n" +
                        "                    \n" +
                        "                    // assert\n" +
                        "                }\n" +
                        "            }\n" +
                        "            public class When_I_try_to_post_to_Gregs_anti_tax_rants : Given_I_am_logged_in_as_Dr_Bill\n" +
                        "            {\n" +
                        "                [Fact]\n" +
                        "                public void Then_I_should_see_Hey_Thats_not_your_blog()\n" +
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
                        "        public class Given_I_am_logged_in_as_Greg : MultipleSiteSupportTest\n" +
                        "        {\n" +
                        "            public class When_I_try_to_post_to_Expensive_Therapy : Given_I_am_logged_in_as_Greg\n" +
                        "            {\n" +
                        "                [Fact]\n" +
                        "                public void Then_I_should_see_Your_article_was_published()\n" +
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