using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Common.Queries;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Queries.FindOrders;

public record FindOrdersQuery : SearchQuery, IRequest<IPagedList<OrderItemModel>>
{
    public FindOrdersQuery(string? branch, bool? isCanceled, int? page, int? pageSize, string? orderBy)
        : base(page, pageSize, orderBy)
    {
        Branch = branch;
        IsCanceled = isCanceled;
    }

    public string? Branch { get; }
    public bool? IsCanceled { get; }
}
