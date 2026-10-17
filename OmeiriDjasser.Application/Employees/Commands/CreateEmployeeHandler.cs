using MediatR;
using OmeiriDjasser.Application.Common.Interfaces;
using OmeiriDjasser.Domain.Entities;

namespace OmeiriDjasser.Application.Employees.Commands
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _repository;

        public CreateEmployeeHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            var employee = new Employee
            {
                Name = request.EmployeeData.Name,
                JobTitle = request.EmployeeData.JobTitle,
                Salary = request.EmployeeData.Salary,
                Age = request.EmployeeData.Age
            };

            return await _repository.AddAsync(employee, ct);
        }
    }
}
