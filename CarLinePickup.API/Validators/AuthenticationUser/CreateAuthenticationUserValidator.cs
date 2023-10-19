using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.API.Validators.AuthenticationUser;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.AuthenticationUser
{
    public class CreateAuthenticationUserValidator : AuthenticationUserValidatorBase<AuthenticationUserRequestCreate>
    {
        private readonly IAuthenticationUserTypeService<Domain.Models.AuthenticationUserType> _authenticationUserTypeService;
        private readonly IAuthenticationUserProviderService<Domain.Models.AuthenticationUserProvider> _authenticationUserProviderService;

        public CreateAuthenticationUserValidator(IAuthenticationUserTypeService<Domain.Models.AuthenticationUserType> authenticationUserTypeService, IAuthenticationUserProviderService<Domain.Models.AuthenticationUserProvider> authenticationUserProviderService) : base(authenticationUserTypeService, authenticationUserProviderService)
        {
            _authenticationUserTypeService = authenticationUserTypeService;
            _authenticationUserProviderService = authenticationUserProviderService;

            RuleFor(authenticationUser => authenticationUser.CreatedBy)
                          .MaximumLength(50)
                          .WithMessage("{PropertyName} can not be greater than 50 characters.");
        }
    }
}
