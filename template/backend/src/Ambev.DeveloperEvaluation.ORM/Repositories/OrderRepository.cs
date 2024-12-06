using Ambev.DeveloperEvaluation.Application.Abstractions.Database;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class OrderRepository(IDefaultContext context) : IOrderRepository
{
    public async Task CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }
}
