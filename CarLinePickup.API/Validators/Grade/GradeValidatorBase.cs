using FluentValidation;
using CarLinePickup.API.Models.Request;
using CarLinePickup.Domain.Services.Interfaces;

namespace CarLinePickup.API.Validators.Grade
{
    public class GradeValidatorBase<T> : AbstractValidator<T> where T : GradeRequestBase
    {
        public GradeValidatorBase()
        {
            RuleFor(grade => grade.Name)
                .MaximumLength(40)
                .WithMessage("{PropertyName} can not be greater than 40 characters.");
        }
    }
}
