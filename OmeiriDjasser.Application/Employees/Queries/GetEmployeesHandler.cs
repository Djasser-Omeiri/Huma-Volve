using MediatR;
using OmeiriDjasser.Application.Employees.DTOs;
using OmeiriDjasser.Application.Common.Interfaces;

namespace OmeiriDjasser.Application.Employees.Queries
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDTO>>
    {
        private readonly IEmployeeRepository _repository;

        public GetEmployeesHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken ct)
        {
            var products = await _repository.GetAllAsync(ct);

            return products.Select(p => new EmployeeDTO
            {
                Id = p.Id,
                Name = p.Name,
                JobTitle = p.JobTitle,
                Salary = p.Salary,
                Age = p.Age
            }).ToList();
        }
    }
}
