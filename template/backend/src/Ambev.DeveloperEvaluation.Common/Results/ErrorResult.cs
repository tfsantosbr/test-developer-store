namespace Ambev.DeveloperEvaluation.Common.Results;

public record ErrorResult : Result
{
    public ErrorResult(Error notification) : base(notification)
    {
    }

    public ErrorResult(Error[] notifications) : base(notifications)
    {
    }
}

public record ErrorResult<TValue> : Result<TValue>
{
    public ErrorResult(Error notification) : base(notification)
    {
    }

    public ErrorResult(Error[] notifications) : base(notifications)
    {
    }
}