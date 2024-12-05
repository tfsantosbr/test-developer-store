using Ambev.DeveloperEvaluation.Application.Orders.Queries.FindOrders;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.Requests;

public record FindOrdersRequest(
    string? Branch, bool? IsCanceled, int? Page, int? PageSize, string? OrderBy)
{
    public FindOrdersQuery ToQuery() =>
        new(Branch, IsCanceled, Page, PageSize, OrderBy);
}


