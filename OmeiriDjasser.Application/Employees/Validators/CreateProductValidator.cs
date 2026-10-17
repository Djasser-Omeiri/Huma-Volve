using FluentValidation;
using OmeiriDjasser.Application.Employees.Commands;

namespace OmeiriDjasser.Application.Employees.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.EmployeeData.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be under 100 characters.");

            RuleFor(x => x.EmployeeData.JobTitle)
                .NotEmpty().WithMessage("Job title is required.");

            RuleFor(x => x.EmployeeData.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.EmployeeData.Age)
                .GreaterThanOrEqualTo(0).WithMessage("Age must be a positive number.");
        }
    }
}
