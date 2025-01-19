using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ResultZero.Benchmarks;

public class Program {
    public static void Main() {
        BenchmarkRunner.Run<Benchmarks>();
    }
}

[MemoryDiagnoser]
public class Benchmarks {
    [Benchmark]
    public void SuccessResultZero() {
        static ResultZero.Result<int> GetResult() {
            return 5;
        }

        if (GetResult().Value != 5) {
            throw new Exception();
        }
    }
    [Benchmark]
    public void SuccessFluentResults() {
        static FluentResults.Result<int> GetResult() {
            return 5;
        }

        if (GetResult().Value != 5) {
            throw new Exception();
        }
    }
    [Benchmark]
    public void SuccessExceptions() {
        static int GetResult() {
            return 5;
        }

        if (GetResult() != 5) {
            throw new Exception();
        }
    }
    [Benchmark]
    public void FailureResultZero() {
        static ResultZero.Result<int> GetResult() {
            return new ResultZero.Error("Example");
        }

        if (!GetResult().IsError) {
            throw new Exception();
        }
    }
    [Benchmark]
    public void FailureFluentResults() {
        static FluentResults.Result<int> GetResult() {
            return FluentResults.Result.Fail("Example");
        }

        if (!GetResult().IsFailed) {
            throw new Exception();
        }
    }
    [Benchmark]
    public void FailureExceptions() {
        static int GetResult() {
            throw new Exception("Example");
        }

        bool Caught = false;
        try {
            GetResult();
        }
        catch (Exception) {
            Caught = true;
        }
        if (!Caught) {
            throw new Exception();
        }
    }
}