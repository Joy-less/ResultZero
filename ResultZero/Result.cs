using System.Diagnostics.CodeAnalysis;

namespace ResultZero;

/// <summary>
/// Success or an error.
/// </summary>
public readonly struct Result : IResult {
    /// <summary>
    /// A successful result.
    /// </summary>
    public static Result Success { get; } = new();

    /// <inheritdoc/>
    public Error ErrorOrDefault { get; }
    /// <inheritdoc/>
    [MemberNotNullWhen(true, nameof(ErrorOrDefault))]
    public bool IsError { get; }

    /// <summary>
    /// Constructs a successful result.
    /// </summary>
    public Result() {
        IsError = false;
    }
    /// <summary>
    /// Constructs a failed result from an error.
    /// </summary>
    public Result(Error Error) {
        ErrorOrDefault = Error;
        IsError = true;
    }
    /// <summary>
    /// Constructs a failed result from an exception.
    /// </summary>
    public Result(Exception Exception)
        : this(new Error(Exception)) {
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException"/>
    public Error Error => IsError ? ErrorOrDefault : throw new InvalidOperationException("Result was value");

    /// <inheritdoc/>
    public override string ToString() {
        if (IsError) {
            return $"Error: {ErrorOrDefault.Message}";
        }
        else {
            return "Success";
        }
    }
    /// <inheritdoc/>
    public bool TryGetError([NotNullWhen(false)] out Error Error) {
        Error = ErrorOrDefault;
        return IsError;
    }
    /// <inheritdoc/>
    /// <exception cref="Exception"/>
    public void ThrowIfError() {
        if (IsError) {
            Error.Throw();
        }
    }

    /// <summary>
    /// Creates a failed result from an error or a successful result from <see langword="null"/>.
    /// </summary>
    public static implicit operator Result(Error? Error) {
        return Error is not null ? new Result(Error.Value) : Success;
    }
    /// <summary>
    /// Creates a failed result from an exception.
    /// </summary>
    public static implicit operator Result(Exception Exception) {
        return Exception is not null ? new Result(Exception) : Success;
    }
}

/// <summary>
/// A value or an error.
/// </summary>
public readonly struct Result<T> : IResult<T> {
    /// <inheritdoc/>
    public T? ValueOrDefault { get; }
    /// <inheritdoc/>
    public Error ErrorOrDefault { get; }
    /// <inheritdoc/>
    [MemberNotNullWhen(true, nameof(ErrorOrDefault))]
    public bool IsError { get; }

    /// <summary>
    /// Constructs a successful result.
    /// </summary>
    public Result(T Value) {
        ValueOrDefault = Value;
        IsError = false;
    }
    /// <summary>
    /// Constructs a failed result from an error.
    /// </summary>
    public Result(Error Error) {
        ErrorOrDefault = Error;
        IsError = true;
    }
    /// <summary>
    /// Constructs a failed result from an exception.
    /// </summary>
    public Result(Exception Exception)
        : this(new Error(Exception)) {
    }

    /// <inheritdoc/>
    [MemberNotNullWhen(true, nameof(ValueOrDefault))]
    public bool IsValue => !IsError;
    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException"/>
    public T Value => IsValue ? ValueOrDefault : throw new InvalidOperationException($"Result was error: \"{Error.Message}\"");
    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException"/>
    public Error Error => IsError ? ErrorOrDefault : throw new InvalidOperationException("Result was value");

    /// <inheritdoc/>
    public override string ToString() {
        if (IsError) {
            return $"Error: {Error.Message}";
        }
        else {
            return $"Success: {Value}";
        }
    }
    /// <inheritdoc/>
    public bool TryGetError([NotNullWhen(false)] out Error Error) {
        Error = ErrorOrDefault;
        return IsError;
    }
    /// <inheritdoc/>
    /// <exception cref="Exception"/>
    public void ThrowIfError() {
        if (IsError) {
            Error.Throw();
        }
    }
    /// <inheritdoc/>
    public Result<TNew> Try<TNew>(Func<T, TNew> Map) {
        return IsValue ? Map(Value) : Error;
    }
    /// <inheritdoc/>
    public bool TryGetValue([NotNullWhen(true)] out T? Value, [NotNullWhen(false)] out Error Error) {
        Value = ValueOrDefault;
        Error = ErrorOrDefault;
        return IsValue;
    }
    /// <inheritdoc/>
    public bool TryGetValue([NotNullWhen(true)] out T? Value) {
        Value = ValueOrDefault;
        return IsValue;
    }
    /// <inheritdoc/>
    public bool ValueEquals(T? Other) {
        return IsValue && Equals(Value, Other);
    }

    /// <summary>
    /// Creates a successful result from a value.
    /// </summary>
    public static implicit operator Result<T>(T Value) {
        return new Result<T>(Value);
    }
    /// <summary>
    /// Creates a failed result from an error.
    /// </summary>
    public static implicit operator Result<T>(Error Error) {
        return new Result<T>(Error);
    }
    /// <summary>
    /// Creates a failed result from an exception.
    /// </summary>
    public static implicit operator Result<T>(Exception Exception) {
        return new Error(Exception);
    }

    /// <inheritdoc/>
    IResult<TNew> IResult<T>.Try<TNew>(Func<T, TNew> Map) => Try(Map);
}