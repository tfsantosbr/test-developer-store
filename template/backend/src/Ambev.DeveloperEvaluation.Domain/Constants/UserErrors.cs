using Ambev.DeveloperEvaluation.Common.Results;

namespace Ambev.DeveloperEvaluation.Domain.Constants;

public static class UserErrors
{
    public static Error UserNotFound(Guid userId) =>
        new(nameof(UserNotFound), $"User not found: '{userId}'");
}
