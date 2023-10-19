using FluentValidation;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.Domain.Services.Interfaces;

namespace CarLinePickup.API.Validators.Grade
{
    public class UpdateGradeValidator : GradeValidatorBase<GradeRequestUpdate>
    {
        public UpdateGradeValidator() : base()
        {

            RuleFor(address => address.ModifiedBy)
                .NotEmpty()
                .MaximumLength(40)
                .WithMessage("{PropertyName} can not be null, empty or greater than 40 characters.");
        }
    }
}
