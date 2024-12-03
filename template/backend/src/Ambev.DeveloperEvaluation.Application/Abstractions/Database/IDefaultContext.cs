using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Abstractions.Database;

public interface IDefaultContext
{
    DbSet<User> Users { get; }
    DbSet<Order> Orders { get; }
}
