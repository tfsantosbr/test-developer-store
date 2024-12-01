namespace Ambev.DeveloperEvaluation.Common.Results;

public record NotFoundResult(Error Notification) : Result(Notification);

public record NotFoundResult<TValue>(Error Notification) : Result<TValue>(Notification);
