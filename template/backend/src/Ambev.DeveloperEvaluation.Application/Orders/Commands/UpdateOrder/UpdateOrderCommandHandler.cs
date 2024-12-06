using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Persistence;
using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(
    IUnitOfWork unitOfWork, IOrderRepository orderRepository, IProductRepository productRepository)
    : IRequestHandler<UpdateOrderCommand, Result>
{
    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.NotFound(OrderErrors.OrderNotFound(request.OrderId));

        order.Update(request.Branch);

        order.ClearItems();

        foreach (var item in request.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId, cancellationToken);

            if (product is null)
                return Result<OrderDetailsModel>.Error(ProductErrors.ProductNotFound(item.ProductId));

            var addItemResult = order.AddItem(product, item.Quantity);

            if (addItemResult.IsFailure)
                return Result<OrderDetailsModel>.Error(addItemResult.Errors);
        }

        order.CalculateDiscount();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}