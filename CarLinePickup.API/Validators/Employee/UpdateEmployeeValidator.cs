using FluentValidation;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.API.Validators.Extensions;

namespace CarLinePickup.API.Validators.Employee
{
    public class UpdateEmployeeValidator : EmployeeValidatorBase<EmployeeRequestUpdate>
    {
        private readonly IEmployeeTypeService<Domain.Models.EmployeeType> _employeeTypeService;

        public UpdateEmployeeValidator(IEmployeeTypeService<Domain.Models.EmployeeType> employeeTypeService) : base(employeeTypeService)
        {
            _employeeTypeService = employeeTypeService;

            RuleFor(employee => employee.ModifiedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");
        }
    }
}
