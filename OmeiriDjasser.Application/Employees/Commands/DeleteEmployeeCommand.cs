using MediatR;

namespace OmeiriDjasser.Application.Employees.Commands
{
    public record DeleteEmployeeCommand(int Id) : IRequest<bool>;
}
