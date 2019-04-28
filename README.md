# Given-When-Then template to C# conversion

## Quick start

Assuming that a feature under test is located at `/c/features/GuessTheWord.feature` with this content

```
Given the Maker has started a game with the word silky
    When the Breaker joins the Maker's game
        Then the Breaker must guess a word with 5 characters
```

Run in the commandline

```
gwt -f /c/features/GuessTheWord.feature -n ProductionCode.Tests
```

Or in the repository's root directory

```
cd src/GivenWhenThenTemplateToCSharp
dotnet run -- -f /c/features/GuessTheWord.feature -n ProductionCode.Tests
```

Generated tests is located at `/c/features/GuessTheWordTest.cs`

```cs
namespace ProductionCode.Tests
{
    public class GuessTheWordTest
    {
        public class Given_the_Maker_has_started_a_game_with_the_word_silky : GuessTheWordTest
        {
            public class When_the_Breaker_joins_the_Makers_game : Given_the_Maker_has_started_a_game_with_the_word_silky
            {
                [Fact]
                public void Then_the_Breaker_must_guess_a_word_with_5_characters()
                {
                    // arrange
                    
                    
                    // act
                    
                    // assert
                }
            }
        }
    }
}
```

Being noted that test class and generated file has a `Test`-suffix.

## Commandline options

```
  -f, --features     Required. Feature files to be converted

  -n, --namespace    (Default: TestsNamespace) Set the namespace of the generated tests

  --help             Display this help screen.

  --version          Display version information.
```

## Credits and references
* [Command Line Parser](https://github.com/commandlineparser/commandline)
* [Structuring Unit Tests](https://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx/)