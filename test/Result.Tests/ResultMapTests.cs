using Shouldly;

namespace LSymds.Result.Tests;

public class ResultMapTests
{
    [Fact]
    public void AppliesTheMappingFunction_WhenTheExistingResultIsSuccessful()
    {
        // Arrange.
        var existingResult = Result<Nothing, string>.Successful(Nothing.Instance);

        // Act.
        var result = existingResult.Map(_ => "Foo");

        // Assert.
        result.IsSuccess.ShouldBeTrue();
        result.IsError.ShouldBeFalse();
        result.Data!.ShouldBe("Foo");
        result.Error.ShouldBeNull();
    }

    [Fact]
    public void LeavesTheErrorUnchanged_WhenTheExistingResultIsErroneous()
    {
        // Arrange.
        var existingResult = Result<Nothing, string>.Erroneous("Oh no.");

        // Act.
        var result = existingResult.Map(_ => "Foo");

        // Assert.
        result.IsSuccess.ShouldBeFalse();
        result.IsError.ShouldBeTrue();
        result.Data!.ShouldBeNull();
        result.Error.ShouldBe("Oh no.");
    }
}
