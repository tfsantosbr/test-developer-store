using Ambev.DeveloperEvaluation.Common.Persistence;
using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem;

public class CancelOrderItemCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    : IRequestHandler<CancelOrderItemCommand, Result>
{
    public async Task<Result> Handle(CancelOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.NotFound(OrderErrors.OrderNotFound(request.OrderId));

        var result = order.CancelItem(request.ItemId);

        if (result.IsFailure)
            return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}