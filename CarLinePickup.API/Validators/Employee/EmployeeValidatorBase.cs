using FluentValidation;
using CarLinePickup.API.Models.Request;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.API.Validators.Extensions;
using System;

namespace CarLinePickup.API.Validators.Employee
{
    public class EmployeeValidatorBase<T> : AbstractValidator<T> where T : EmployeeRequestBase
    {
        private readonly IEmployeeTypeService<Domain.Models.EmployeeType> _employeeTypeService;

        public EmployeeValidatorBase(IEmployeeTypeService<Domain.Models.EmployeeType> employeeTypeService)
        {
            _employeeTypeService = employeeTypeService;

            When(employee => employee.EmployeeTypeId != Guid.Empty, () =>
            {
                RuleFor(employee => employee.EmployeeTypeId)
                    .Must((obj, employeeTypeId) => (_employeeTypeService.GetAsync(employeeTypeId).Result != null))
                    .WithMessage("Please supply a valid {PropertyName}.");
            });

            RuleFor(employee => employee.FirstName)
                          .MaximumLength(100)
                          .WithMessage("{PropertyName} can not be greater than 50 characters.");

            RuleFor(employee => employee.LastName)
                          .MaximumLength(100)
                          .WithMessage("{PropertyName} can not be greater than 50 characters.");

            RuleFor(employee => employee.Email)
                          .MaximumLength(50)
                          .WithMessage("{PropertyName} can not be greater than 50 characters.");

            RuleFor(employee => employee.Email).EmailAddress()
              .WithMessage("Please provide a valid email address.");
        }
    }
}
