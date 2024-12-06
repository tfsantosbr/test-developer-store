using Ambev.DeveloperEvaluation.Application.Abstractions.Database;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

public static class ProductsEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("products").WithTags("Products");

        group.MapGet("/", async (IDefaultContext context, CancellationToken cancellationToken = default) =>
            Results.Ok(await context.Products.ToListAsync(cancellationToken)));
    }
}
