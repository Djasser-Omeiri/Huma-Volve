using OmeiriDjasser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmeiriDjasser.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> AddAsync(Employee employee, CancellationToken ct = default);
        Task<Employee?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<Employee>> GetAllAsync(CancellationToken ct = default);

        Task<bool> UpdateAsync(int id, Employee employee, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);

    }
}
