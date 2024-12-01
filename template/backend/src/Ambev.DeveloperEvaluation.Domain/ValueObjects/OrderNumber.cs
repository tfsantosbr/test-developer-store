namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public sealed record OrderNumber(int Value)
{
    public static OrderNumber Create()
    {
        var year = DateTime.UtcNow.Year;
        var number = new Random().Next(10000000, 99999999);

        return new OrderNumber(year * 100000000 + number);
    }
}
