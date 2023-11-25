using Shouldly;

namespace LSymds.Result.Tests;

public class ResultMapErrorTests
{
    [Fact]
    public void AppliesTheMappingResult_WhenTheExistingResultIsErroneous()
    {
        // Arrange.
        var existingResult = Result<Nothing, string>.Erroneous("Oh no.");
        
        // Act.
        var newResult = existingResult.MapError(e => e.Length);
        
        // Assert.
        newResult.IsSuccess.ShouldBeFalse();
        newResult.IsError.ShouldBeTrue();
        newResult.Error.ShouldBe(6);
        newResult.Data.ShouldBeNull();
    }

    [Fact]
    public void LeavesTheDataUnchanged_WhenTheExistingResultIsSuccessful()
    {
        // Arrange.
        var existingResult = Result<string, string>.Successful("Success");
        
        // Act.
        var newResult = existingResult.MapError(_ => "Oh no.");
        
        // Assert.
        newResult.IsSuccess.ShouldBeTrue();
        newResult.IsError.ShouldBeFalse();
        newResult.Data.ShouldBe("Success");
        newResult.Error.ShouldBeNull();
    }
}