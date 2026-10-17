using MediatR;
using OmeiriDjasser.Application.Employees.DTOs;
namespace OmeiriDjasser.Application.Employees.Queries
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDTO?>;
}
