namespace ResultZero.Tests;

public class ReadmeTests {
    [Fact]
    public void Test1() {
        // Unwrap results, throwing if error
        Console.WriteLine(Divide(10, 2).Value);

        // Check if results errored
        if (Divide(10, 0).IsError) {
            Console.WriteLine("Denominator was 0");
        }

        // Advanced error handling
        if (Divide(3, 1).TryGetValue(out int Value, out Error Error)) {
            Console.WriteLine(Value);
        }
        else {
            Error.Throw();
        }
    }

    private static Result<int> Divide(int Numerator, int Denominator) {
        if (Denominator == 0) {
            return new Error("Cannot divide by zero.");
        }
        return Numerator / Denominator;
    }
}