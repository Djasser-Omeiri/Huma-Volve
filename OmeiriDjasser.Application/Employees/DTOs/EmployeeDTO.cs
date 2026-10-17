namespace OmeiriDjasser.Application.Employees.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int Age { get; set; }
    }
}
