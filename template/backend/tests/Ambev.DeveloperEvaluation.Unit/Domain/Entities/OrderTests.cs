using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class OrderTests
{
    [Fact(DisplayName = "AddItem should return error when quantity is less than or equal to zero")]
    public void AddItem_ShouldReturnError_WhenQuantityIsLessThanOrEqualToZero()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product = new Product("Product1", 10);

        // Act
        var result = order.AddItem(product, 0);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(OrderErrors.CantAddItemWithQuantityLessThanZero(), result.Errors);
    }

    [Fact(DisplayName = "AddItem should return error when order has more than 20 items of the same product")]
    public void AddItem_ShouldReturnError_WhenOrderHasMoreThan20ItemsOfSameProduct()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product = new Product("Product1", 10);
        order.AddItem(product, 20);

        // Act
        var result = order.AddItem(product, 1);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(OrderErrors.OrderHasMoreThan20ItemsOfSameProduct(product.Id), result.Errors);
    }

    [Fact(DisplayName = "AddItem should add item when valid")]
    public void AddItem_ShouldAddItem_WhenValid()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product = new Product("Product1", 10);

        // Act
        var result = order.AddItem(product, 5);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "CalculateDiscount should add 20% discount when item quantities are between 10 and 20")]
    public void CalculateDiscount_ShouldAdd20PercentDiscount_WhenItemQuantitiesBetween10And20()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product = new Product("Product1", 10);
        order.AddItem(product, 15);

        // Act
        order.CalculateDiscount();

        // Assert
        Assert.Single(order.Discounts);
        Assert.Equal(20, order.Discounts.First().Percentage);
    }

    [Fact(DisplayName = "CalculateDiscount should add 10% discount when item quantities are between 4 and 9")]
    public void CalculateDiscount_ShouldAdd10PercentDiscount_WhenItemQuantitiesBetween4And9()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product = new Product("Product1", 10);
        order.AddItem(product, 5);

        // Act
        order.CalculateDiscount();

        // Assert
        Assert.Single(order.Discounts);
        Assert.Equal(10, order.Discounts.First().Percentage);
    }

    [Fact(DisplayName = "CalculateDiscount should not add discount when item quantities are less than 4")]
    public void CalculateDiscount_ShouldNotAddDiscount_WhenItemQuantitiesLessThan4()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product = new Product("Product1", 10);
        order.AddItem(product, 3);

        // Act
        order.CalculateDiscount();

        // Assert
        Assert.Empty(order.Discounts);
    }

    [Fact(DisplayName = "TotalWithDiscount should return correct total when discounts are applied")]
    public void TotalWithDiscount_ShouldReturnCorrectTotal_WhenDiscountsApplied()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product = new Product("Product1", 10);
        order.AddItem(product, 10);
        order.CalculateDiscount();

        // Act
        var totalWithDiscount = order.TotalWithDiscount;

        // Assert
        Assert.Equal(80, totalWithDiscount); // 10 items * $10 each - 20% discount
    }

    [Fact(DisplayName = "Quantities should return correct total quantity")]
    public void Quantities_ShouldReturnCorrectTotalQuantity()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "User1" };
        var order = Order.Create(user, "Branch1");
        var product1 = new Product("Product1", 10);
        var product2 = new Product("Product2", 20);
        order.AddItem(product1, 5);
        order.AddItem(product2, 3);

        // Act
        var quantities = order.Quantities;

        // Assert
        Assert.Equal(8, quantities);
    }
}