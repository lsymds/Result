using Shouldly;

namespace LSymds.Result.Tests;

public class ResultUnwrapOrElseTests
{
    [Fact]
    public void ReturnsTheValueOfData_WhenTheResultIsSuccessful()
    {
        // Arrange.
        var result = Result<string, int>.Successful("Success");

        // Act.
        var value = result.UnwrapOrElse("Or");

        // Assert.
        value.ShouldBe("Success");
    }

    [Fact]
    public void ReturnsTheAlternativeValue_WhenTheResultIsErroneous()
    {
        // Arrange.
        var result = Result<string, int>.Erroneous(10);

        // Act.
        var value = result.UnwrapOrElse("Or");

        // Assert.
        value.ShouldBe("Or");
    }
}
