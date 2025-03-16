namespace ResultZero;

/// <summary>
/// An error that occurred.
/// </summary>
public readonly struct Error {
    /// <summary>
    /// The error code for debugging purposes.
    /// </summary>
    public object? Code { get; }
    /// <summary>
    /// The error message for debugging purposes.
    /// </summary>
    public object? Message { get; }

    /// <summary>
    /// Constructs an error with a code and a message.
    /// </summary>
    public Error(object? Code, object? Message) {
        this.Code = Code;
        this.Message = Message;
    }
    /// <summary>
    /// Constructs an error with a message.
    /// </summary>
    public Error(object? Message)
        : this(null, Message) {
    }
    /// <summary>
    /// Constructs an error.
    /// </summary>
    public Error()
        : this(null) {
    }

    /// <summary>
    /// Returns the error code cast as <typeparamref name="T"/>.
    /// </summary>
    public T GetCode<T>() {
        return (T)Code!;
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