using FluentValidation;
using OrdersAPI.Commands;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Models;
namespace OrdersAPI.Handlers;

public class CreateOrderCommandHandler :ICommandHandler<CreateOrderCommand, OrderDto>
{
    private readonly AppDbContext _appDbContext;
    private readonly IValidator<CreateOrderCommand> _validator;

    public CreateOrderCommandHandler(AppDbContext appDbContext, IValidator<CreateOrderCommand> validator)
    {
        _appDbContext = appDbContext;
        _validator = validator;
    }
    //public static async Task<Order> Handle(CreateOrderCommand command, AppDbContext dbContext)
    //{
    //    var order = new Order
    //    {
    //        FirstName = command.FirstName,
    //        LastName = command.LastName,
    //        TotalAmount = command.TotalAmount
    //    };

    //    await dbContext.Orders.AddAsync(order);
    //    await dbContext.SaveChangesAsync();
    //    return order;
    //}

    public async Task<OrderDto> HandleAsync(CreateOrderCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);

        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var order = new Order
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            TotalAmount = command.TotalAmount
        };

        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();

        return new OrderDto(order.Id, order.FirstName, order.LastName, order.CreatedAt, order.TotalAmount);
    }
}
