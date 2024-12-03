using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Queries.GetOrderDetails;

public record GetOrderDetailsQuery(Guid OrderId) : IRequest<Result<OrderDetailsModel>>;
