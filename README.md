*Given-When-Then template to C#* is a commandline tool to convert given-when-then clauses to [xUnit.net](https://github.com/xunit/xunit) tests. The structure of unit tests is inspired by [this](https://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx/) and [this](http://zendeveloper.blogspot.com/2012/01/structuring-unit-tests.html) article.

[![Build status](https://ci.appveyor.com/api/projects/status/7gxvggk4r1aauun9/branch/master?svg=true)](https://ci.appveyor.com/project/sonbua/givenwhenthentemplatetocsharp/branch/master)

## Quick start

Assuming that a feature is described in `/path/to/features/GuessTheWord.feature` with this content

```
Given the Maker has started a game with the word silky
    When the Breaker joins the Maker's game
        Then the Breaker must guess a word with 5 characters
```

Run this in the repository's root directory

```
cd src/GivenWhenThenTemplateToCSharp
dotnet run -- --features /path/to/features/GuessTheWord.feature --namespace ProductionCode.Tests
```

Or in the commandline where `gwt.exe` is located

```
gwt --features /path/to/features/GuessTheWord.feature --namespace ProductionCode.Tests
```

Test file is generated in the same directory as the feature file, i.e. `/path/to/features/GuessTheWordTest.cs`

```c#
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

Being noted that both test class and generated file have a `Test`-suffix.

Multiple given-when-then in a feature are also supported. Optionally use blank lines to make the specs easier to read.

```
Given I am logged in as Dr. Bill
    When I try to post to "Expensive Therapy"
        Then I should see "Your article was published."
    When I try to post to "Greg's anti-tax rants"
        Then I should see "Hey! That's not your blog!"

Given I am logged in as Greg
    When I try to post to "Expensive Therapy"
        Then I should see "Your article was published."
```

## Commandline options

```
  -f, --features     Required. Feature files to be converted

  -n, --namespace    (Default: TestsNamespace) Set the namespace of the generated tests

  --help             Display this help screen.

  --version          Display version information.
```

## Notes

To build this console app as Windows 10 executable (`.exe`), run this command

```
dotnet build -c Release -r win10-x64
```

## Credits and references
* [Command Line Parser](https://github.com/commandlineparser/commandline)
* Structuring Unit Tests [here](https://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx/) and [here](http://zendeveloper.blogspot.com/2012/01/structuring-unit-tests.html)
* [Generate an exe for .NET Core Console Apps: .NET Core Quick Posts Part V](https://dzone.com/articles/generate-an-exe-for-net-core-console-apps-net-core)