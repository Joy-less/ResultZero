namespace ResultZero.Tests;

public class BasicTests {
    [Fact]
    public void Test1() {
        Assert.Equal(5, ExampleSuccess().Value);

        Assert.Throws<InvalidOperationException>(() => ExampleFailure().Value);
        Assert.Throws<Exception>(() => ExampleFailure().ThrowIfError());

        Assert.Throws<InvalidOperationException>(() => ExampleFailureException().Value);
        Assert.Throws<InvalidDataException>(() => ExampleFailureException().ThrowIfError());
    }

    private static Result<int> ExampleSuccess() {
        return 5;
    }
    private static Result<int> ExampleFailure() {
        return new Error("Not 5 lol.");
    }
    private static Result<int> ExampleFailureException() {
        return new Error(new InvalidDataException("Not 5 but an exception."));
    }
}