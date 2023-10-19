using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.API.Validators.Address;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.Address
{
    public class CreateAddressValidator : AddressValidatorBase<AddressRequestCreate>
    {
        public CreateAddressValidator(ICountyService<Domain.Models.County> countyService, IStateService<Domain.Models.State> stateService) : base(countyService, stateService)
        {
            RuleFor(address => address.AddressOne).NotEmpty()
                .WithMessage("{PropertyName} can not be null.");

            RuleFor(address => address.City).NotEmpty()
                .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(address => address.CountyId).NotEmpty()
                .WithMessage("{PropertyName} can not be null.");

            RuleFor(address => address.StateId).NotEmpty()
                .WithMessage("{PropertyName} can not be null.");

            RuleFor(address => address.Zip).NotEmpty()
                .WithMessage("{PropertyName} can not be null or blank.");

            RuleFor(address => address.CreatedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");
        }
    }
}
