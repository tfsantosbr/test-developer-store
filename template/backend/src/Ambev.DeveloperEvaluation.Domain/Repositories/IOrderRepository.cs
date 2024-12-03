﻿using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IOrderRepository
{
    Task CreateAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
}
