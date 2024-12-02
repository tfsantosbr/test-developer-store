namespace Ambev.DeveloperEvaluation.Common.Results;

public record Result
{
    protected Result()
    {
    }

    protected Result(Error notification) =>
        Errors = [notification];

    protected Result(Error[] notifications) =>
        Errors = notifications;

    public Error[] Errors { get; } = [];
    public bool IsSuccess => Errors.Length == 0;
    public bool IsFailure => !IsSuccess;

    public static ErrorResult Error(Error notification) => new(notification);
    public static ErrorResult Error(Error[] notifications) => new(notifications);
    public static NotFoundResult NotFound(Error notification) => new(notification);
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

    internal Result(Error notification)
        : base(notification)
    {
    }

    internal Result(Error[] notifications)
        : base(notifications)
    {
    }

    public static new ErrorResult<TData> Error(Error notification) => new(notification);
    public static new ErrorResult<TData> Error(Error[] notifications) => new(notifications);
    public static new NotFoundResult<TData> NotFound(Error notification) => new(notification);
    public static Result<TData> Success(TData data) => new(data);
}