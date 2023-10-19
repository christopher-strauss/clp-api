using System;
using FluentValidation;
using CarLinePickup.API.Models.Request;
using CarLinePickup.Domain.Services.Interfaces;

namespace CarLinePickup.API.Validators.Address
{
    public class AddressValidatorBase<T> : AbstractValidator<T> where T : AddressRequestBase
    {
        private readonly ICountyService<Domain.Models.County> _countyService;
        private readonly IStateService<Domain.Models.State> _stateService;

        public AddressValidatorBase(ICountyService<Domain.Models.County> countyService, IStateService<Domain.Models.State> stateService)   
        {
            _countyService = countyService;
            _stateService = stateService;

            RuleFor(address => address.AddressOne)
                .MaximumLength(255)
                .WithMessage("{PropertyName} can not greater than 255 characters.");

            RuleFor(address => address.City).MaximumLength(255)
                .WithMessage("{PropertyName} can not be greater than 255 characters.");

            When(address => address.CountyId != Guid.Empty, () =>
            {
                RuleFor(address => address.CountyId)
                    .Must((obj, countyId) => (_countyService.GetAsync(countyId).Result != null))
                    .WithMessage("Please supply a valid {PropertyName}.");

            });

            When(address => address.StateId != Guid.Empty, () =>
            {
                RuleFor(address => address.StateId)
                .Must((obj, stateId) => (_stateService.GetAsync(stateId).Result != null))
                .WithMessage("Please supply a valid {PropertyName}.");
            });


            When(address => (address.StateId != Guid.Empty || address.CountyId != Guid.Empty), () =>
            {
                RuleFor(address => address)
                    .Must((obj) => (_countyService.GetByCountyIdAndStateId(obj.CountyId, obj.StateId).Result != null))
                    .WithMessage("Please supply a valid State and County combination.");
            });

            RuleFor(address => address.Zip)
                .Matches("^[0-9]{5}(?:-[0-9]{4})?$")
                .WithMessage("{PropertyName} must be in a five digit or ZIP+4 format.");
        }
    }
}
