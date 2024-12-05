using Ambev.DeveloperEvaluation.Application.Abstractions.Database;
using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Orders.Queries.FindOrders;

public class FindOrdersQueryHandler(IDefaultContext context) : IRequestHandler<FindOrdersQuery, IPagedList<OrderItemModel>>
{
    public async Task<IPagedList<OrderItemModel>> Handle(
        FindOrdersQuery query, CancellationToken cancellationToken = default)
    {
        var orders = context.Orders.AsNoTracking();

        orders = Filter(query, orders);
        orders = Order(query, orders);

        var total = await orders.CountAsync(cancellationToken: cancellationToken);
        var items = await orders
            .Page(query.PageNumber, query.PageSize)
            .Select(order => OrderItemModel.FromOrder(order))
            .ToListAsync(cancellationToken: cancellationToken);

        var pagedItems = new PagedList<OrderItemModel>(items, total, query.PageNumber, query.PageSize);

        return pagedItems;
    }

    // Private Methods

    private static IQueryable<Order> Order(FindOrdersQuery query, IQueryable<Order> queryable)
    {
        queryable = query.OrderBy switch
        {
            "branch-asc" => queryable.OrderBy(u => u.Branch),
            "branch-desc" => queryable.OrderByDescending(u => u.Branch),
            _ => queryable.OrderBy(u => u.Branch),
        };

        return queryable;
    }

    private static IQueryable<Order> Filter(FindOrdersQuery query, IQueryable<Order> queryable)
    {
        if (query.Branch is not null)
            queryable = queryable.Where(p => p.Branch.StartsWith(query.Branch));

        if (query.IsCanceled is not null)
            queryable = queryable.Where(p => p.IsCanceled == query.IsCanceled);

        return queryable;
    }
}
