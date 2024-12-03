using Ambev.DeveloperEvaluation.Common.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem;

public record CancelOrderItemCommand(Guid OrderId, Guid ItemId) : IRequest<Result>;
