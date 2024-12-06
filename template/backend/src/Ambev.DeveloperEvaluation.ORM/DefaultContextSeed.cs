
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContextSeed(DefaultContext context)
{
    public void SeedData()
    {
        SeedProducts();
    }

    private void SeedProducts()
    {
        if(context.Products.Any())
            return;

        context.Products.AddRange(
            new Product("Skol 600ml", 5.5m),
            new Product("Brahma 600ml", 5.5m),
            new Product("Heineken 600ml", 7.0m),
            new Product("Antarctica 600ml", 5.5m),
            new Product("Bohemia 600ml", 6.0m),
            new Product("Stella Artois 600ml", 7.5m),
            new Product("Corona 600ml", 7.5m),
            new Product("Original 600ml", 6.0m),
            new Product("Skol 300ml", 3.0m),
            new Product("Brahma 300ml", 3.0m),
            new Product("Heineken 300ml", 4.0m),
            new Product("Antarctica 300ml", 3.0m),
            new Product("Bohemia 300ml", 3.5m),
            new Product("Stella Artois 300ml", 4.0m),
            new Product("Corona 300ml", 4.0m),
            new Product("Original 300ml", 3.5m),
            new Product("Skol 1L", 8.0m),
            new Product("Brahma 1L", 8.0m),
            new Product("Heineken 1L", 9.0m),
            new Product("Antarctica 1L", 8.0m),
            new Product("Bohemia 1L", 8.5m),
            new Product("Stella Artois 1L", 9.0m),
            new Product("Corona 1L", 9.0m),
            new Product("Original 1L", 8.5m)
        );

        context.SaveChanges();
    }
}
