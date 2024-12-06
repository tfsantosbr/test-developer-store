using Ambev.DeveloperEvaluation.Common.Results;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public static class ResultExtensions
{
    // Extension Methods

    public static IResult Ok<TValue>(this Result<TValue> result) =>
        BaseResult(result, TypedResults.Ok(result.Data));

    public static IResult NoContent(this Result result) =>
        BaseResult(result, TypedResults.NoContent());

    public static IResult Accepted(this Result result, string? uri = null) =>
        BaseResult(result, TypedResults.Accepted(uri));

    public static IResult Created<TValue>(this Result<TValue> result, string? uri = null) =>
        BaseResult(result, TypedResults.Created(uri, result.Data));

    // Private Methods

    private static IResult BaseResult(Result result, IResult successResult)
    {
        if (result.IsSuccess)
            return successResult;

        return result.Type switch
        {
            ResultType.NotFound => TypedResults.NotFound(result.Errors),
            ResultType.Error => TypedResults.BadRequest(result.Errors),
            _ => throw new InvalidOperationException(),
        };
    }

    private static IResult BaseResult<TValue>(Result<TValue> result, IResult successResult)
    {
        if (result.IsSuccess)
            return successResult;

        return result.Type switch
        {
            ResultType.NotFound => TypedResults.NotFound(result.Errors),
            ResultType.Error => TypedResults.BadRequest(result.Errors),
            _ => throw new InvalidOperationException(),
        };
    }
}
