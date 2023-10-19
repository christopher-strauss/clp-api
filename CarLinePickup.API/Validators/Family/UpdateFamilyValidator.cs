using FluentValidation;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.Domain.Services.Interfaces;

namespace CarLinePickup.API.Validators.Family
{
    public class UpdateFamilyValidator : FamilyValidatorBase<FamilyRequestUpdate>
    {
        public UpdateFamilyValidator(ICountyService<Domain.Models.County> countyService, IStateService<Domain.Models.State> stateService) : base(countyService, stateService)
        {
            RuleFor(address => address.ModifiedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be greater than 50 characters.");
        }
    }
}
