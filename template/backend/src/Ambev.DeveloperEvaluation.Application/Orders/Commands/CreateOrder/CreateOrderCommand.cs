using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Common.Results;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid UserId, string Branch, CreateOrderCommandItem[] Items) : IRequest<Result<OrderDetailsModel>>;

public record CreateOrderCommandItem(Guid ProductId, int Quantity);
