using Ambev.DeveloperEvaluation.Application.Abstractions.Database;
using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQueryHandler(IDefaultContext context) : IRequestHandler<GetOrderDetailsQuery, Result<OrderDetailsModel>>
{
    public async Task<Result<OrderDetailsModel>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await context.Orders
            .Include(order => order.Items)
            .Include(order => order.Discounts)
            .AsNoTracking()
            .Where(order => order.Id == request.OrderId)
            .Select(order => OrderDetailsModel.FromOrder(order))
            .FirstOrDefaultAsync(cancellationToken);

        if (orderDetails is null)
            return Result<OrderDetailsModel>.NotFound(OrderErrors.OrderNotFound(request.OrderId));

        return Result.Success(orderDetails);
    }
}