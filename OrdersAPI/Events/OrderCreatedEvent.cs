namespace OrdersAPI.Events
{
    public record OrderCreatedEvent
    (
        int Orderid,
        string FirstName,
        string LastName,
        decimal TotalAmount
    );
}
