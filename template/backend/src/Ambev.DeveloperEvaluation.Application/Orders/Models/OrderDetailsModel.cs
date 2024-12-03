﻿using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Orders.Models;

public record OrderDetailsModel(Guid UserId, string Branch, OrderItem[] Items, Discount[] Discounts)
{
    public static OrderDetailsModel FromOrder(Order order)
    {
        var orderCreatedModel = new OrderDetailsModel(
            order.UserId, 
            order.Branch, 
            order.Items.Select(item => new OrderItem(
                item.ProductId, 
                item.Quantity, 
                item.UnitPrice, 
                item.TotalPrice)
            ).ToArray(), 
            order.Discounts.Select(discount => 
                new Discount(discount.Percentage)
            ).ToArray()
        );

        return orderCreatedModel;
    }
}

public record OrderItem(Guid ProductId, int Quantity, decimal UnitPrice, decimal TotalPrice);

public record Discount(int Percentage);