using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Persistence;
using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IUnitOfWork unitOfWork, IUserRepository userRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    : IRequestHandler<CreateOrderCommand, Result<OrderDetailsModel>>
{
    public async Task<Result<OrderDetailsModel>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result<OrderDetailsModel>.Error(UserErrors.UserNotFound(request.UserId));

        var order = Order.Create(user, request.Branch);

        foreach (var item in request.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId, cancellationToken);

            if (product is null)
                return Result<OrderDetailsModel>.Error(ProductErrors.ProductNotFound(item.ProductId));

            var addItemResult = order.AddItem(product, item.Quantity);

            if(addItemResult.IsFailure)
                return Result<OrderDetailsModel>.Error(addItemResult.Errors);
        }

        order.CalculateDiscount();

        await orderRepository.CreateAsync(order, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var orderCreatedModel = OrderDetailsModel.FromOrder(order);

        return Result<OrderDetailsModel>.Success(orderCreatedModel);
    }
}