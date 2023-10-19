using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.Grade
{
    public class CreateGradeValidator : GradeValidatorBase<GradeRequestCreate>
    {
        public CreateGradeValidator() : base()
        {
            RuleFor(grade => grade.Name).NotEmpty()
                .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(grade => grade.CreatedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");
        }
    }
}
