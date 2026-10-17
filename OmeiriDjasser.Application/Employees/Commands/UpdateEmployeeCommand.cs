using MediatR;
using OmeiriDjasser.Application.Employees.DTOs;

namespace OmeiriDjasser.Application.Employees.Commands
{
    public record UpdateEmployeeCommand(int Id, CreateEmployeeDTO EmployeeData) : IRequest<bool>;
}
