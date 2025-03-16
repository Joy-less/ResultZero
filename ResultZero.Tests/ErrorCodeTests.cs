namespace ResultZero.Tests;

public class ErrorCodeTests {
    [Fact]
    public void Test1() {
        Error Error = new(ErrorTest.Green, null);
        Error.Code.ShouldBe(ErrorTest.Green);
        Error.GetCode<ErrorTest>().ShouldBe(ErrorTest.Green);
    }

    private enum ErrorTest : short {
        Red,
        Green,
        Blue,
    }
}