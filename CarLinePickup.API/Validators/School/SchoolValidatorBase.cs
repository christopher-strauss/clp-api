using FluentValidation;
using CarLinePickup.API.Models.Request;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.API.Validators.Address;

namespace CarLinePickup.API.Validators.School
{
    public class SchoolValidatorBase<T> : AbstractValidator<T> where T : SchoolRequestBase
    {
        private readonly ICountyService<Domain.Models.County> _countyService;
        private readonly IStateService<Domain.Models.State> _stateService;

        public SchoolValidatorBase(ICountyService<Domain.Models.County> countyService, IStateService<Domain.Models.State> stateService)   
        {
            _countyService = countyService;
            _stateService = stateService;

            RuleFor(school => school.Name)
                .MaximumLength(100)
                .WithMessage("{PropertyName} can not be greater than 100 characters.");
        }
    }
}
