using System.Runtime.CompilerServices;

namespace ResultZero;

/// <summary>
/// An error that occurred.
/// </summary>
public readonly struct Error {
    /// <summary>
    /// The optional error code (or 0).
    /// </summary>
    public long Code { get; }
    /// <summary>
    /// The error message for debugging purposes.
    /// </summary>
    public object? Message { get; }

    /// <summary>
    /// Constructs an error that occurred with an error code.
    /// </summary>
    public Error(long Code, object? Message) {
        this.Code = Code;
        this.Message = Message;
    }
    /// <summary>
    /// Constructs an error that occurred.
    /// </summary>
    public Error(object? Message)
        : this(0, Message) {
    }

    /// <summary>
    /// Returns the error code cast as <typeparamref name="T"/>.
    /// </summary>
    public T GetCode<T>() where T : struct {
        return Unsafe.BitCast<long, T>(Code);
    }

    /// <summary>
    /// Returns the error message cast as <typeparamref name="T"/>.
    /// </summary>
    public T GetMessage<T>() {
        return (T)Message!;
    }

    /// <summary>
    /// If <see cref="Message"/> is an exception, it is thrown.<br/>
    /// Otherwise, an <see cref="Exception"/> is thrown with the error message.
    /// </summary>
    /// <exception cref="Exception"/>
    public void Throw() {
        throw (Message as Exception) ?? new Exception(ToString());
    }

    /// <summary>
    /// Returns a string representation of the error.
    /// </summary>
    public override string ToString() {
        return $"Error: \"{Message}\"";
    }

    /// <summary>
    /// Creates an error from an exception.
    /// </summary>
    public static implicit operator Error(Exception Exception) {
        return new Error(Exception);
    }
}