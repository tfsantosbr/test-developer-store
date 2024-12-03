using Ambev.DeveloperEvaluation.Common.Results;

namespace Ambev.DeveloperEvaluation.Domain.Constants;

public static class ProductErrors
{
    public static Error ProductNotFound(Guid productId) =>
        new(nameof(ProductNotFound), $"Product not found: '{productId}'");
}
