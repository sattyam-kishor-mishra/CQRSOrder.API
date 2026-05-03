using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Commands;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Handlers;
using OrdersAPI.Models;
using OrdersAPI.Queries;


namespace OrdersAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersControllerController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IQueryHandler<GetOrderByIdQuery, OrderDto> _getOrderByIdQueryHandler;
    private readonly IQueryHandler<GetAllOrders, List<OrderDto>> _getAllOrdersQueryHandler;
    private readonly ICommandHandler<CreateOrderCommand, OrderDto> _createOrderCommandHandler;

    public OrdersControllerController(AppDbContext appDbContext, 
        IQueryHandler<GetOrderByIdQuery, OrderDto> getOrderByIdQueryHandler, 
        IQueryHandler<GetAllOrders, List<OrderDto>> getAllOrdersQueryHandler, 
        ICommandHandler<CreateOrderCommand, OrderDto> createOrderCommandHandler)
    {
        _appDbContext = appDbContext;
        _getOrderByIdQueryHandler = getOrderByIdQueryHandler;
        _getAllOrdersQueryHandler = getAllOrdersQueryHandler;
        _createOrderCommandHandler = createOrderCommandHandler;
    }
    [HttpPost("saveOrder")]
    public async Task<IActionResult> SaveOrderAsync(CreateOrderCommand command)
    {
        // await _appDbContext.Orders.AddAsync(order);
        // await _appDbContext.SaveChangesAsync();

        //var createdOrder = await CreateOrderCommandHandler.Handle(command, _appDbContext);
        try
        {
            var createdOrder = await _createOrderCommandHandler.HandleAsync(command);
            return Ok(createdOrder);
        }
        catch (ValidationException ex)
        {
            var error = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(error);
        }        
    }

    [HttpGet("getOrders/{id}")]
    public async Task<IActionResult> GetOrdersByIdAsync(int id)
    {
        //var order = await _appDbContext.Orders.FindAsync(id);

        //var order = await GetOrderByIdQueryHandler.Handle(new GetOrderByIdQuery(id), _appDbContext);

        var order = await _getOrderByIdQueryHandler.HandleAsync(new GetOrderByIdQuery(id));

        return Ok(order);
    }

    [HttpGet("getOrders")]
    public async Task<IActionResult> GetAllOrderAsync()
    {
        //List<Order> orders = await _appDbContext.Orders.ToListAsync();
        //var orders = await GetOrderByIdQueryHandler.Handle(_appDbContext);

        var orders = await _getAllOrdersQueryHandler.HandleAsync(new GetAllOrders());
        return Ok(orders);
    }
}   
