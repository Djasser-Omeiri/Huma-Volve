using Microsoft.EntityFrameworkCore;
using OmeiriDjasser.Infrastructure.Data;
using OmeiriDjasser.Application.Common.Interfaces;
using OmeiriDjasser.Infrastructure.Repositories;
using OmeiriDjasser.Application.Employees.Commands;
using OmeiriDjasser.Application.Employees.Validators;
using OmeiriDjasser.Application.Common.Behaviors;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeHandler).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateEmployeeValidator).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();




