using OrdersAPI.Data;
using OrdersAPI.Models;
using OrdersAPI.Queries;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Dtos;
namespace OrdersAPI.Handlers;

public class GetOrderByIdQueryHandler :IQueryHandler<GetOrderByIdQuery, OrderDto>, IQueryHandler<GetAllOrders, List<OrderDto>>
{
    private readonly AppDbContext _appDbContext;

    public GetOrderByIdQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    //public static async Task<Order?> Handle(GetOrderByIdQuery query, AppDbContext dbContext)
    //{
    //    return await dbContext.Orders.FindAsync(query.Id);
    //}

    //public static async Task<List<Order>> Handle(AppDbContext dbContext)
    //{
    //    return await dbContext.Orders.ToListAsync();
    //}

    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await _appDbContext.Orders.FindAsync(query.Id);
        if (order == null) return null;
        return new OrderDto(order.Id, order.FirstName, order.LastName, order.CreatedAt, order.TotalAmount);
    }

    public Task<List<OrderDto>?> HandleAsync(GetAllOrders query)
    {
        return _appDbContext.Orders.Select(order => new OrderDto(order.Id, order.FirstName, order.LastName, order.CreatedAt, order.TotalAmount)).ToListAsync()!;
    }
}
