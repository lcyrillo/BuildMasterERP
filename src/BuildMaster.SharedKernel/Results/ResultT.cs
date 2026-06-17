using BuildMaster.SharedKernel.Errors;

namespace BuildMaster.SharedKernel.Results;

public class Result<T> : Result
{
    private readonly T? _value;

    protected Result(T? value)
        : base(true, Error.None)
    {
        _value = value;
    }

    protected Result(Error error)
        : base(false, error)
    {
        _value = default;
    }

    public T Value =>
        IsSuccess
            ? _value!
            : throw new InvalidOperationException(
                "The value of a failure result cannot be accessed.");

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public static new Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }
}
