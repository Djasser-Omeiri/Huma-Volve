using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmeiriDjasser.Application.Employees.Commands;
using OmeiriDjasser.Application.Employees.Queries;
using OmeiriDjasser.Application.Employees.DTOs;
using Serilog;

namespace OmeiriDjasser.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // POST /api/employees
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO dto)
        {
            _logger.LogInformation("Creating new employee with name: {EmployeeName} and salary: {EmployeeSalary}", dto.Name, dto.Salary);
            try
            {
                var command = new CreateEmployeeCommand(dto);
                var id = await _mediator.Send(command);
                _logger.LogInformation("Employee created successfully with ID: {EmployeeId}", id);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee with name: {EmployeeName}", dto.Name);
                throw;
            }
        }

        // PUT /api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEmployeeDTO dto)
        {
            _logger.LogInformation("Updating employee with ID: {EmployeeId}, name: {EmployeeName}", id, dto.Name);
            try
            {
                var command = new UpdateEmployeeCommand(id, dto);
                var result = await _mediator.Send(command);
                if (!result)
                {
                    _logger.LogWarning("Employee not found with ID: {EmployeeId}", id);
                    return NotFound();
                }
                _logger.LogInformation("Employee updated successfully with ID: {EmployeeId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee with ID: {EmployeeId}", id);
                throw;
            }
        }

        // GET /api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching employee with ID: {EmployeeId}", id);
            try
            {
                var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
                if (employee == null)
                {
                    _logger.LogWarning("Employee not found with ID: {EmployeeId}", id);
                    return NotFound();
                }
                _logger.LogInformation("Employee retrieved successfully with ID: {EmployeeId}", id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee with ID: {EmployeeId}", id);
                throw;
            }
        }

        // GET /api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all employees");
            try
            {
                var employees = await _mediator.Send(new GetEmployeesQuery());
                _logger.LogInformation("Retrieved {EmployeeCount} employees", employees.Count());
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all employees");
                throw;
            }
        }

        // DELETE /api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting employee with ID: {EmployeeId}", id);
            try
            {
                var result = await _mediator.Send(new DeleteEmployeeCommand(id));
                if (!result)
                {
                    _logger.LogWarning("Employee not found with ID: {EmployeeId}", id);
                    return NotFound();
                }
                _logger.LogInformation("Employee deleted successfully with ID: {EmployeeId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee with ID: {EmployeeId}", id);
                throw;
            }
        }
    }
}
