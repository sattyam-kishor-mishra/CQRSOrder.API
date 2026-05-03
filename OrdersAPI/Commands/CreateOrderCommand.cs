namespace OrdersAPI.Commands;

public record CreateOrderCommand(string FirstName, string LastName, decimal TotalAmount);