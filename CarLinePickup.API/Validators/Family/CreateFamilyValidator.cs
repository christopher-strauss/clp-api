using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.API.Validators.Address;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.Family
{
    public class CreateFamilyValidator : FamilyValidatorBase<FamilyRequestCreate>
    {
        private readonly ICountyService<Domain.Models.County> _countyService;
        private readonly IStateService<Domain.Models.State> _stateService;

        public CreateFamilyValidator(ICountyService<Domain.Models.County> countyService, IStateService<Domain.Models.State> stateService) : base(countyService, stateService)
        {
            _countyService = countyService;
            _stateService = stateService;

            RuleFor(family => family.Name).NotEmpty()
                 .WithMessage("{PropertyName} can not be null or empty.");
            
            RuleFor(family => family.Address).NotEmpty()
                .WithMessage("{PropertyName} can not be null.");

            RuleFor(family => family.CreatedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");


            RuleFor(family => family.Address).SetValidator(new CreateAddressValidator(_countyService, _stateService));
        }
    }
}
