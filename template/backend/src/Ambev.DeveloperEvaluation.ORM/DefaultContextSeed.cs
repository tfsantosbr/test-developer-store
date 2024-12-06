
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContextSeed(DefaultContext context)
{
    public void SeedData()
    {
        SeedUsers();
        SeedProducts();
    }

    private void SeedUsers()
    {
        if (context.Users.Any())
            return;

        context.Users.AddRange(
            new User
            {
                Id = new Guid("5da91f12-e38b-430c-8202-61c24c263f91"),
                Username = "admin",
                Password = "admin",
                Email = "admin@email.com",
                Phone = "1199999999",
                Role = UserRole.Admin,
                Status = UserStatus.Active
            }
        );
    }

    private void SeedProducts()
    {
        if (context.Products.Any())
            return;

        context.Products.AddRange(
            new Product("Skol 600ml", 5.5m, new Guid("64846d8c-1271-4fc3-b488-af13acf36a53")),
            new Product("Brahma 600ml", 5.5m, new Guid("23291fc1-f094-4583-a58d-6524552136a5")),
            new Product("Heineken 600ml", 7.0m, new Guid("1ca4bce0-7c79-4804-aa31-9dbc5304aff7"))
        );

        context.SaveChanges();
    }
}
