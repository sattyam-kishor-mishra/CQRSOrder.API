using static System.Runtime.InteropServices.JavaScript.JSType;
namespace OrdersAPI.Models;

public class Order
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
}
