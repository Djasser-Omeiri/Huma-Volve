using MediatR;
using OmeiriDjasser.Application.Employees.DTOs;

namespace OmeiriDjasser.Application.Employees.Commands
{
    public record CreateEmployeeCommand(CreateEmployeeDTO EmployeeData) : IRequest<int>;
}
