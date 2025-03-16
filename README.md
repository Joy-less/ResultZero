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

| Method               | Mean          | Error     | StdDev    | Median        | Gen0   | Allocated |
|--------------------- |--------------:|----------:|----------:|--------------:|-------:|----------:|
| SuccessResultZero    |     1.9425 ns | 0.0088 ns | 0.0083 ns |     1.9417 ns |      - |         - |
| SuccessFluentResults |    56.6426 ns | 0.1629 ns | 0.1360 ns |    56.5849 ns | 0.0510 |     160 B |
| SuccessExceptions    |     0.0028 ns | 0.0030 ns | 0.0028 ns |     0.0022 ns |      - |         - |
| FailureResultZero    |     0.0016 ns | 0.0022 ns | 0.0020 ns |     0.0010 ns |      - |         - |
| FailureFluentResults |   205.3345 ns | 0.7756 ns | 0.6876 ns |   205.3714 ns | 0.1938 |     608 B |
| FailureExceptions    | 2,576.1063 ns | 7.6907 ns | 6.8176 ns | 2,575.9346 ns | 0.0992 |     320 B |