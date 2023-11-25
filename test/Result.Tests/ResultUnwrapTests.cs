using Shouldly;

namespace LSymds.Result.Tests;

public class ResultUnwrapTests
{
    [Fact]
    public void ReturnsTheValueOfData_WhenTheResultIsSuccessful()
    {
        // Arrange.
        var result = Result<string, int>.Successful("Success");

        // Act.
        var value = result.Unwrap();

        // Assert.
        value.ShouldBe("Success");
    }

    [Fact]
    public void ThrowsAnException_WhenTheResultIsErroneous()
    {
        // Arrange.
        var result = Result<string, int>.Erroneous(10);

        // Act.
        var func = () => result.Unwrap();

        // Assert.
        func.ShouldThrow<InvalidOperationException>();
    }
}
