namespace ResultZero.Tests;

public class BasicTests {
    [Fact]
    public void Test1() {
        ExampleSuccess().Value.ShouldBe(5);

        Should.Throw<InvalidOperationException>(() => ExampleFailure().Value);
        Should.Throw<Exception>(() => ExampleFailure().ThrowIfError());

        Should.Throw<InvalidOperationException>(() => ExampleFailureException().Value);
        Should.Throw<InvalidDataException>(() => ExampleFailureException().ThrowIfError());
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