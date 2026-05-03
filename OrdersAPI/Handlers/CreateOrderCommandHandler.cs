using FluentValidation;
using OrdersAPI.Commands;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Events;
using OrdersAPI.Models;
namespace OrdersAPI.Handlers;

public class CreateOrderCommandHandler :ICommandHandler<CreateOrderCommand, OrderDto>
{
    private readonly WriteDbContext _context;
    private readonly IValidator<CreateOrderCommand> _validator;
    private readonly IEventPublisher _eventPublisher;

    public CreateOrderCommandHandler(WriteDbContext context, IValidator<CreateOrderCommand> validator, IEventPublisher eventPublisher)
    {
        _context = context;       
        _validator = validator;
        _eventPublisher = eventPublisher;        
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

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        var orderCreatedEvent = new OrderCreatedEvent
            (
                order.Id,
                order.FirstName,
                order.LastName,
                order.TotalAmount
            );

        await _eventPublisher.PublishAsync(orderCreatedEvent);

        return new OrderDto(order.Id, order.FirstName, order.LastName, order.CreatedAt, order.TotalAmount);
    }
}
