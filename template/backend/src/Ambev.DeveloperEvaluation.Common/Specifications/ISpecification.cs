namespace Ambev.DeveloperEvaluation.Common.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}
