using MediatR;
using OmeiriDjasser.Application.Common.Interfaces;
using OmeiriDjasser.Domain.Entities;

namespace OmeiriDjasser.Application.Employees.Commands
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _repository;

        public UpdateEmployeeHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken ct)
        {
            var employee = new Employee
            {
                Id = request.Id,
                Name = request.EmployeeData.Name,
                JobTitle = request.EmployeeData.JobTitle,
                Salary = request.EmployeeData.Salary,
                Age = request.EmployeeData.Age
            };

            return await _repository.UpdateAsync(request.Id, employee, ct);
        }
    }
}
