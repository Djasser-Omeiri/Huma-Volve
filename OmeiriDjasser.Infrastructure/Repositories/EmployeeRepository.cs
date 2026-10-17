using OmeiriDjasser.Infrastructure.Data;
using OmeiriDjasser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OmeiriDjasser.Application.Common.Interfaces;

namespace OmeiriDjasser.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Employee employee, CancellationToken ct = default)
        {
            await _context.Employees.AddAsync(employee, ct);
            await _context.SaveChangesAsync(ct);
            return employee.Id;
        }

        public async Task<Employee?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<bool> UpdateAsync(int id, Employee employee, CancellationToken ct = default)
        {
            var existingEmployee = await GetByIdAsync(id, ct);
            if (existingEmployee == null)
                return false;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var existingEmployee = await GetByIdAsync(id, ct);
            if (existingEmployee == null)
                return false;

            _context.Employees.Remove(existingEmployee);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
