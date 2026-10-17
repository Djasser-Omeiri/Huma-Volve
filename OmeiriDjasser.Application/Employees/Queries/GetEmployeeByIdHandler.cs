using MediatR;
using OmeiriDjasser.Application.Employees.DTOs;
using OmeiriDjasser.Application.Common.Interfaces;

namespace OmeiriDjasser.Application.Employees.Queries
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO?>
    {
        private readonly IEmployeeRepository _repository;

        public GetEmployeeByIdHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeDTO?> Handle(GetEmployeeByIdQuery request, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(request.Id, ct);
            if (product == null) return null;

            return new EmployeeDTO
            {
                Id = product.Id,
                Name = product.Name,
                JobTitle = product.JobTitle,
                Salary = product.Salary,
                Age = product.Age
            };
        }
    }
}
