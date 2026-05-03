
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;
using OrdersAPI.Events;
using OrdersAPI.Models;

namespace OrdersAPI.Projections
{
    public class OrderCreatedProjectionHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly ReadDbContext _context;

        public OrderCreatedProjectionHandler(ReadDbContext context)
        {
            _context = context;
        }
        public async Task HandleAsync(OrderCreatedEvent @event)
        {
            var order = new Order
            {
                FirstName = @event.FirstName,
                LastName = @event.LastName,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = @event.TotalAmount
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
