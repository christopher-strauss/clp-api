using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.API.Validators.Extensions;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.Employee
{
    public class CreateEmployeeValidator : EmployeeValidatorBase<EmployeeRequestCreate>
    {
        private readonly IEmployeeTypeService<Domain.Models.EmployeeType> _employeeTypeService;

        public CreateEmployeeValidator(IEmployeeTypeService<Domain.Models.EmployeeType> employeeTypeService) : base(employeeTypeService)
        {
            _employeeTypeService = employeeTypeService;

            RuleFor(employee => employee.FirstName).NotEmpty()
                          .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(employee => employee.LastName).NotEmpty()
                          .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(employee => employee.Email).NotEmpty()
                          .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(employee => employee.EmployeeTypeId).NotEmpty()
                          .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(employee => employee.CreatedBy)
                          .NotEmpty()
                          .MaximumLength(50)
                          .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");
        }
    }
}
