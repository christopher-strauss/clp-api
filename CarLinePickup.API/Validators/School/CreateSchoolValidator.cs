using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.API.Validators.Address;
using CarLinePickup.API.Validators.School;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.School
{
    public class CreateSchoolValidator : SchoolValidatorBase<SchoolRequestCreate>
    {
        private readonly ICountyService<Domain.Models.County> _countyService;
        private readonly IStateService<Domain.Models.State> _stateService;

        public CreateSchoolValidator(ICountyService<Domain.Models.County> countyService, IStateService<Domain.Models.State> stateService) : base(countyService, stateService)
        {
            _countyService = countyService;
            _stateService = stateService;

            RuleFor(school => school.Name).NotEmpty()
                .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(address => address.CreatedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");

            RuleFor(school => school.Address).SetValidator(new CreateAddressValidator(_countyService, _stateService)); RuleFor(school => school.Address).SetValidator(new CreateAddressValidator(_countyService, _stateService));
        }
    }
}
