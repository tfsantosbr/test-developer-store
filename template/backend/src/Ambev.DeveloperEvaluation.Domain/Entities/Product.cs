using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public sealed class Product : BaseEntity
{
    public Product(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }

    private Product()
    {
    }

    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
}
