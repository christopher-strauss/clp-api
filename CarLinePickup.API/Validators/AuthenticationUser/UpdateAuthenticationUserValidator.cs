using FluentValidation;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.Domain.Services.Interfaces;

namespace CarLinePickup.API.Validators.AuthenticationUser
{
    public class UpdateAuthenticationUserValidator : AuthenticationUserValidatorBase<AuthenticationUserRequestUpdate>
    {
        private readonly IAuthenticationUserTypeService<Domain.Models.AuthenticationUserType> _authenticationUserTypeService;
        private readonly IAuthenticationUserProviderService<Domain.Models.AuthenticationUserProvider> _authenticationUserProviderService;

        public UpdateAuthenticationUserValidator(IAuthenticationUserTypeService<Domain.Models.AuthenticationUserType> authenticationUserTypeService, IAuthenticationUserProviderService<Domain.Models.AuthenticationUserProvider> authenticationUserProviderService) : base(authenticationUserTypeService, authenticationUserProviderService)
        {
            _authenticationUserTypeService = authenticationUserTypeService;
            _authenticationUserProviderService = authenticationUserProviderService;

            RuleFor(authenticationUser => authenticationUser.ModifiedBy)
                          .MaximumLength(50)
                          .WithMessage("{PropertyName} can not be greater than 50 characters.");
        }
    }
}
