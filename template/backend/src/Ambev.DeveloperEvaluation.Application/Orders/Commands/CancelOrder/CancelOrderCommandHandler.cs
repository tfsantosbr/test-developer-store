using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Persistence;
using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    : IRequestHandler<CancelOrderCommand, Result>
{
    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.NotFound(OrderErrors.OrderNotFound(request.OrderId));

        order.Cancel();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}