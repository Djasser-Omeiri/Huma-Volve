using MediatR;
using OmeiriDjasser.Application.Common.Interfaces;

namespace OmeiriDjasser.Application.Employees.Commands
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _repository;

        public DeleteEmployeeHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken ct)
        {
            return await _repository.DeleteAsync(request.Id, ct);
        }
    }
}
