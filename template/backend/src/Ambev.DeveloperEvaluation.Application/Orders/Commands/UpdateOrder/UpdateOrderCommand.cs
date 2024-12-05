using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid OrderId, string Branch, UpdateOrderCommandItem[] Items) : IRequest<Result>;

public record UpdateOrderCommandItem(Guid ProductId, int Quantity);
