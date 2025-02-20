using System.Diagnostics.CodeAnalysis;

namespace ResultZero;

/// <summary>
/// Success or an error.
/// </summary>
public interface IResult {
    /// <summary>
    /// Returns the error that occurred or <see langword="default"/>(<see cref="Error"/>).
    /// </summary>
    public Error ErrorOrDefault { get; }
    /// <summary>
    /// Returns the error that occurred or throws an exception.
    /// </summary>
    public Error Error { get; }
    /// <summary>
    /// Returns <see langword="true"/> if an error occurred.
    /// </summary>
    public bool IsError { get; }
    /// <summary>
    /// Returns <see langword="true"/> if an error occurred and provides the error or <see langword="default"/>(<see cref="Error"/>).
    /// </summary>
    public bool TryGetError([NotNullWhen(true)] out Error Error);
    /// <summary>
    /// Throws an exception if an error occurred.
    /// </summary>
    public void ThrowIfError();
    /// <summary>
    /// Returns a string representation of the result.
    /// </summary>
    public string ToString();
}

/// <summary>
/// A value or an error.
/// </summary>
public interface IResult<T> : IResult {
    /// <summary>
    /// Returns the value or <see langword="default"/>(<typeparamref name="T"/>).
    /// </summary>
    public T? ValueOrDefault { get; }
    /// <summary>
    /// Returns the value or throws an exception.
    /// </summary>
    public T Value { get; }
    /// <summary>
    /// Returns <see langword="true"/> if a value was successfully returned.
    /// </summary>
    public bool IsValue { get; }
    /// <summary>
    /// Transforms the value using a mapping function if a value was successfully returned or returns the error.
    /// </summary>
    public IResult<TNew> Try<TNew>(Func<T, TNew> Map);
    /// <summary>
    /// Returns <see langword="true"/> if an error occurred and provides the error or <see langword="default"/>(<see cref="Error"/>)
    /// and the value or <see langword="default"/>(<typeparamref name="T"/>).
    /// </summary>
    public bool TryGetError([NotNullWhen(true)] out Error Error, [NotNullWhen(false)] out T? Value);
    /// <summary>
    /// Returns <see langword="true"/> if a value was successfully returned and provides the value or <see langword="default"/>(<typeparamref name="T"/>)
    /// and the error or <see langword="default"/>(<see cref="Error"/>).
    /// </summary>
    public bool TryGetValue([NotNullWhen(true)] out T? Value, [NotNullWhen(false)] out Error Error);
    /// <summary>
    /// Returns <see langword="true"/> if a value was successfully returned and provides the value or <see langword="default"/>(<typeparamref name="T"/>).
    /// </summary>
    public bool TryGetValue([NotNullWhen(true)] out T? Value);
    /// <summary>
    /// Returns <see langword="true"/> if a value was successfully returned and is equal to <paramref name="Other"/>.
    /// </summary>
    public bool ValueEquals(T? Other);
}