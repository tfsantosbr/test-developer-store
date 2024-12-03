using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid OrderId, string Branch, OrderItem[] Items) : IRequest<Result>;

public record OrderItem(Guid ProductId, int Quantity);
