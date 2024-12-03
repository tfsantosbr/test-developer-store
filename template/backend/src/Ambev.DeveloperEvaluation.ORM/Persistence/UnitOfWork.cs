using Ambev.DeveloperEvaluation.Common.Persistence;

namespace Ambev.DeveloperEvaluation.ORM.Persistence;

public class UnitOfWork(DefaultContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
