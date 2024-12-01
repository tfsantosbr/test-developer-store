using Ambev.DeveloperEvaluation.Domain.Constants;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class OrderTests
{
    [Fact(DisplayName = "AddItem should add item when valid")]
    public void AddItem_ShouldAddItem_WhenValid()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "Branch1");
        var productId = Guid.NewGuid();

        // Act
        var result = order.AddItem(productId, 5);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "AddItem should return error when has more than 20 items of the same product")]
    public void AddItem_ShouldReturnError_WhenHasMoreThan20ItemsOfSameProduct()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "Branch1");
        var productId = Guid.NewGuid();
        order.AddItem(productId, 20);

        // Act
        var result = order.AddItem(productId, 1);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(OrderErrors.OrderHasMoreThan20ItemsOfSameProduct(productId), result.Notifications);
    }

    [Fact(DisplayName = "CalculateDiscount should apply 20% discount when items between 10 and 20")]
    public void CalculateDiscount_ShouldApply20PercentDiscount_WhenItemsBetween10And20()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "Branch1");
        for (int i = 0; i < 10; i++)
        {
            order.AddItem(Guid.NewGuid(), 1);
        }

        // Act
        order.CalculateDiscount();

        // Assert
        Assert.Single(order.Discounts);
        Assert.Equal(20, order.Discounts.First().Percentage);
    }

    [Fact(DisplayName = "CalculateDiscount should apply 10% discount when items between 4 and 9")]
    public void CalculateDiscount_ShouldApply10PercentDiscount_WhenItemsBetween4And9()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "Branch1");
        for (int i = 0; i < 4; i++)
        {
            order.AddItem(Guid.NewGuid(), 1);
        }

        // Act
        order.CalculateDiscount();

        // Assert
        Assert.Single(order.Discounts);
        Assert.Equal(10, order.Discounts.First().Percentage);
    }

    [Fact(DisplayName = "TotalWithDiscount should return correct total when discounts applied")]
    public void TotalWithDiscount_ShouldReturnCorrectTotal_WhenDiscountsApplied()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), "Branch1");
        var productId = Guid.NewGuid();
        var product = new Product("Product1", 100);
        order.AddItem(productId, 10);
        order.CalculateDiscount();

        // Act
        var totalWithDiscount = order.TotalWithDiscount;

        // Assert
        Assert.Equal(800, totalWithDiscount); // 1000 - 20% discount
    }
}

