namespace Ambev.DeveloperEvaluation.Common.Pagination;

public static class PaginationExtension
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        => query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}
