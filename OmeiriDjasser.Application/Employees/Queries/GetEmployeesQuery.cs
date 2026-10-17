using MediatR;
using OmeiriDjasser.Application.Employees.DTOs;

namespace OmeiriDjasser.Application.Employees.Queries
{
    public record GetEmployeesQuery() : IRequest<List<EmployeeDTO>>;
}
