using System;
using FluentValidation;
using CarLinePickup.API.Models.Request;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.API.Validators.Extensions;

namespace CarLinePickup.API.Validators.AuthenticationUser
{
    public class AuthenticationUserValidatorBase<T> : AbstractValidator<T> where T : AuthenticationUserRequestBase
    {
        private readonly IAuthenticationUserTypeService<Domain.Models.AuthenticationUserType> _authenticationUserTypeService;
        private readonly IAuthenticationUserProviderService<Domain.Models.AuthenticationUserProvider> _authenticationUserProviderService;

        public AuthenticationUserValidatorBase(IAuthenticationUserTypeService<Domain.Models.AuthenticationUserType> authenticationUserTypeService, IAuthenticationUserProviderService<Domain.Models.AuthenticationUserProvider> authenticationUserProviderService)   
        {
            _authenticationUserTypeService = authenticationUserTypeService;
            _authenticationUserProviderService = authenticationUserProviderService;

            RuleFor(authenticationUser => authenticationUser.ExternalProviderId).NotEmpty()
                          .MaximumLength(100)
                          .WithMessage("{PropertyName} can not be null, empty or greater than 100 characters.");

            When(authenticationUser => authenticationUser.AuthenticationUserTypeId != Guid.Empty, () =>
            {
                RuleFor(authenticationUser => authenticationUser.AuthenticationUserTypeId)
                    .Must((obj, authenticationUserTypeId) => (_authenticationUserTypeService.GetAsync(authenticationUserTypeId).Result != null))
                    .WithMessage("Please supply a valid {PropertyName}.");
            });

            When(authenticationUser => !String.IsNullOrEmpty(authenticationUser.ProviderType), () =>
            {
                RuleFor(authenticationUser => authenticationUser.ProviderType)
                    .Must((obj, providerType) => (_authenticationUserProviderService.GetByNameAsync(providerType).Result != null))
                    .WithMessage("Please supply a valid {PropertyName}.");
            });
        }
    }
}
