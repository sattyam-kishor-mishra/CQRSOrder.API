using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Commands;
using OrdersAPI.Data;
using OrdersAPI.Dtos;
using OrdersAPI.Handlers;
using OrdersAPI.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();

builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto>, GetOrderByIdQueryHandler>();

builder.Services.AddScoped<IQueryHandler<GetAllOrders, List<OrderDto>>, GetOrderByIdQueryHandler>();

builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
