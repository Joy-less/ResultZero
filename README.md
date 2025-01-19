# ResultZero

[![NuGet](https://img.shields.io/nuget/v/ResultZero.svg)](https://www.nuget.org/packages/ResultZero)

Zero-allocation result pattern for C#.

Normally, exceptions are a great choice for error handling in C#.
However, they can be notoriously slow when used for control flow. The result pattern can be better for:
- Parsers that are expected to handle erroneous input
- Validating user input in server APIs

ResultZero is designed with the following goals in mind:
- Zero memory allocation for both success and error cases
- No redundant features or bloat
- Scalable for all use cases

## Example

```cs
static Result<int> Divide(int Numerator, int Denominator) {
    if (Denominator == 0) {
        return new Error("Cannot divide by zero.");
    }
    return Numerator / Denominator;
}

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
```

## Benchmarks

Comparison between [ResultZero](https://github.com/Joy-less/ResultZero), [FluentResults](https://github.com/altmann/FluentResults) and exceptions:

| Method               | Mean          | Error      | StdDev     | Median        | Gen0   | Allocated |
|--------------------- |--------------:|-----------:|-----------:|--------------:|-------:|----------:|
| SuccessResultZero    |     1.9420 ns |  0.0118 ns |  0.0111 ns |     1.9411 ns |      - |         - |
| SuccessFluentResults |    56.0877 ns |  0.4140 ns |  0.3457 ns |    56.0095 ns | 0.0510 |     160 B |
| SuccessExceptions    |     0.0015 ns |  0.0029 ns |  0.0028 ns |     0.0000 ns |      - |         - |
| FailureResultZero    |     0.0020 ns |  0.0039 ns |  0.0036 ns |     0.0000 ns |      - |         - |
| FailureFluentResults |   206.8702 ns |  1.0955 ns |  0.9148 ns |   206.9100 ns | 0.1938 |     608 B |
| FailureExceptions    | 2,622.5916 ns | 12.3147 ns | 10.9167 ns | 2,623.9363 ns | 0.0992 |     320 B |