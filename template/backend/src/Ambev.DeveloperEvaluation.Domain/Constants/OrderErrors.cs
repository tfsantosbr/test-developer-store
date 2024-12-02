using Ambev.DeveloperEvaluation.Common.Results;

namespace Ambev.DeveloperEvaluation.Domain.Constants;

public static class OrderErrors
{
    public static Error OrderHasMoreThan20ItemsOfSameProduct(Guid productId) =>
        new(nameof(OrderHasMoreThan20ItemsOfSameProduct), $"Order has more than 20 items of the same product: '{productId}'");

    public static Error CantAddItemWithQuantityLessThanZero() =>
        new(nameof(CantAddItemWithQuantityLessThanZero), "Can't add item with quantity less than zero");
}
