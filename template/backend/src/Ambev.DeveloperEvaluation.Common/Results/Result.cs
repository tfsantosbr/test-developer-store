namespace Ambev.DeveloperEvaluation.Common.Results;

public record Result
{
    protected Result()
    {
    }

    protected Result(Error error, ResultType? type = null)
    {
        Errors = [error];
        Type = type ?? ResultType.Error;
    }

    protected Result(Error[] errors) =>
        Errors = errors;

    public Error[] Errors { get; } = [];
    public ResultType Type { get; }
    public bool IsSuccess => Errors.Length == 0;
    public bool IsFailure => !IsSuccess;

    public static Result Error(Error error) => new(error);
    public static Result Error(Error[] errors) => new(errors);
    public static Result NotFound(Error error) => new(error, ResultType.NotFound);
    public static Result Success() => new();
    public static Result<TData> Success<TData>(TData data) => new(data);
}

public record Result<TData> : Result
{
    public TData? Data { get; }

    internal Result(TData data)
    {
        Data = data;
    }

    internal Result(Error error, ResultType? type = null)
        : base(error, type)
    {
    }

    internal Result(Error[] errors)
        : base(errors)
    {
    }

    public static new Result<TData> Error(Error error) => new(error);
    public static new Result<TData> Error(Error[] errors) => new(errors);
    public static new Result<TData> NotFound(Error error) => new(error, ResultType.NotFound);
    public static Result<TData> Success(TData data) => new(data);
}

public enum ResultType
{
    Error,
    NotFound,
}