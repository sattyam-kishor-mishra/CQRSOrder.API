namespace OrdersAPI.Dtos
{
    public record OrderDto
    (
        int Id,
        string FirstName,
        string LastName,
        DateTime CreatedAt,
        Decimal TotalAmount
    );
}
