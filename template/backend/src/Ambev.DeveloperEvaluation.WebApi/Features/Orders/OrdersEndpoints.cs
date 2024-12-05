using Ambev.DeveloperEvaluation.Application.Orders.Commands.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Models;
using Ambev.DeveloperEvaluation.Application.Orders.Queries.FindOrders;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Common.Results;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.Requests;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Orders.Queries.GetOrderDetails;
using Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Commands.CancelOrderItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders;

public static class OrdersEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("orders").WithTags("Orders");

        group.MapGet("/", FindOrders)
            .Produces<IPagedList<OrderItemModel>>();

        group.MapPost("/", CreateOrder)
            .Produces<OrderDetailsModel>(StatusCodes.Status201Created)
            .Produces<List<Error>>(StatusCodes.Status400BadRequest);

        group.MapGet("/{orderId}", GetOrderDetails)
            .Produces<OrderDetailsModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{orderId}", UpdateOrder)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<List<Error>>(StatusCodes.Status400BadRequest);

        group.MapDelete("/{orderId}/items/{itemId}", CancelOrderItem)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{orderId}", CancelOrder)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> FindOrders(
        [AsParameters] FindOrdersRequest request,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var pagedOrderItems = await mediator.Send(query, cancellationToken);

        return Results.Ok(pagedOrderItems);
    }

    public static async Task<IResult> CreateOrder(
        CreateOrderRequest request,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await mediator.Send(command, cancellationToken);

        return result.Created($"orders/{result.Data!.Id}");
    }

    public static async Task<IResult> GetOrderDetails(
        Guid orderId,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var query = new GetOrderDetailsQuery(orderId);

        var result = await mediator.Send(query, cancellationToken);

        return result.Ok();
    }

    public static async Task<IResult> UpdateOrder(
        Guid orderId,
        UpdateOrderRequest request,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await mediator.Send(command, cancellationToken);

        return result.NoContent();
    }

    public static async Task<IResult> CancelOrder(
        Guid orderId,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var command = new CancelOrderCommand(orderId);

        var result = await mediator.Send(command, cancellationToken);

        return result.NoContent();
    }

    public static async Task<IResult> CancelOrderItem(
        Guid orderId,
        Guid itemId,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var command = new CancelOrderItemCommand(orderId, itemId);

        var result = await mediator.Send(command, cancellationToken);

        return result.NoContent();
    }
}
