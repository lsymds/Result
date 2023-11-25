using Shouldly;

namespace LSymds.Result.Tests;

public class ResultCreationTests
{
    [Fact]
    public void CreatesASuccessfulResult()
    {
        // Act.
        var result = Result<Nothing, Exception>.Successful(Nothing.Instance);
        
        // Assert.
        result.IsSuccess.ShouldBeTrue();
        result.IsError.ShouldBeFalse();
        result.Data.ShouldBe(Nothing.Instance);
        result.Error.ShouldBeNull();
    }

    [Fact]
    public void CreatesAnErroneousResult()
    {
        // Act.
        var result = Result<Nothing, string>.Erroneous("Oh no.");
        
        // Assert.
        result.IsSuccess.ShouldBeFalse();
        result.IsError.ShouldBeTrue();
        result.Data.ShouldBeNull();
        result.Error.ShouldBe("Oh no.");
    }
}