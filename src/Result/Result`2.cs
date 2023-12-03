using System.Diagnostics.CodeAnalysis;

namespace LSymds.Result;

/// <summary>
/// A monad representing either a successful result or an erroneous result.
/// </summary>
/// <typeparam name="TSuccess">The type of the successful result's data.</typeparam>
/// <typeparam name="TError">The type of the erroneous result's error.</typeparam>
public record Result<TSuccess, TError>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="Result{TSuccess,TError}"/> class in a successful state.
    /// </summary>
    /// <param name="success">The success data.</param>
    internal Result(TSuccess success)
    {
        IsSuccess = true;
        IsError = false;
        Data = success;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Result{TSuccess,TError}"/> in an errored state.
    /// </summary>
    /// <param name="erroneous">The error.</param>
    internal Result(TError erroneous)
    {
        IsSuccess = false;
        IsError = true;
        Error = erroneous;
    }

    /// <summary>
    /// Gets the data when <see cref="IsSuccess"/> is true. Will return null if it is not.
    /// </summary>
    public TSuccess? Data { get; }

    /// <summary>
    /// Gets the error when <see cref="IsError"/> is true. Will return null if it is not.
    /// </summary>
    public TError? Error { get; }

    /// <summary>
    /// Gets whether or not the result is a successful result.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Data))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets whether or not the result is an erroneous result.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    [MemberNotNullWhen(false, nameof(Data))]
    public bool IsError { get; set; }

    /// <summary>
    /// Maps the existing <see cref="Result{TSuccess,TError}"/> to a new <see cref="Result{TSuccess,TError}"/>, applying
    /// the provided function to the existing successful data value and leaving the error value (if present) unchanged.
    /// </summary>
    /// <param name="dataMapper">A function used to map the successful data of type TSuccess to a type of TNewSuccess.</param>
    /// <typeparam name="TNewSuccess">The new type of the successful data value.</typeparam>
    public Result<TNewSuccess, TError> Map<TNewSuccess>(Func<TSuccess, TNewSuccess> dataMapper)
    {
        return IsSuccess
            ? new Result<TNewSuccess, TError>(dataMapper(Data!))
            : new Result<TNewSuccess, TError>(Error!);
    }

    /// <summary>
    /// Maps the existing <see cref="Result{TSuccess,TError}"/> to a new <see cref="Result{TSuccess,TError}"/>, applying
    /// the provided function to the existing error value and leaving the success value (if present) unchanged.
    /// </summary>
    /// <param name="errorMapper">A function used to map the error data of type TError to a type of a TNewError.</param>
    /// <typeparam name="TNewError">The new type of the error value.</typeparam>
    public Result<TSuccess, TNewError> MapError<TNewError>(Func<TError, TNewError> errorMapper)
    {
        return IsError
            ? new Result<TSuccess, TNewError>(errorMapper(Error!))
            : new Result<TSuccess, TNewError>(Data!);
    }

    /// <summary>
    /// Unsafely extracts the value of <see cref="Data"/>, throwing an <see cref="InvalidOperationException"/> if
    /// <see cref="IsSuccess"/> is false.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="IsSuccess"/> is false.</exception>
    public TSuccess Unwrap()
    {
        if (!IsSuccess)
        {
            throw new InvalidOperationException(
                "Unable to retrieve Data value as Result is in an erroneous state."
            );
        }

        return Data!;
    }

    /// <summary>
    /// Safely extracts the value of <see cref="Data"/> if <see cref="IsSuccess"/> is true, otherwise returning
    /// <see cref="or"/>.
    /// </summary>
    /// <param name="or">The alternative value to return when <see cref="IsSuccess"/> is false.</param>
    public TSuccess UnwrapOrElse(TSuccess or)
    {
        return IsSuccess ? Data : or;
    }

    /// <summary>
    /// Creates a <see cref="Result{TSuccess,TError}"/> in a successful state with the provided data.
    /// </summary>
    /// <param name="success">The data to be included within the result.</param>
    public static Result<TSuccess, TError> Successful(TSuccess success)
    {
        return new Result<TSuccess, TError>(success);
    }

    /// <summary>
    /// Creates a <see cref="Result{TSuccess,TError}"/> in an erroneous state with the provided error.
    /// </summary>
    /// <param name="error">The error to be included within the result.</param>
    public static Result<TSuccess, TError> Erroneous(TError error)
    {
        return new Result<TSuccess, TError>(error);
    }
}
