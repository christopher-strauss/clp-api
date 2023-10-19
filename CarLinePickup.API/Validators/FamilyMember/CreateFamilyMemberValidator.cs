using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.API.Validators.Extensions;
using CarLinePickup.API.Validators.FamilyMember;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.FamilyMember
{
    public class CreateFamilyMemberValidator : FamilyMemberValidatorBase<FamilyMemberRequestCreate>
    {
        private readonly IFamilyMemberTravelTypeService<Domain.Models.FamilyMemberTravelType> _familyMemberTravelTypeService;
        private readonly IFamilyMemberTypeService<Domain.Models.FamilyMemberType> _familyMemberTypeService;

        public CreateFamilyMemberValidator(IFamilyMemberTravelTypeService<Domain.Models.FamilyMemberTravelType> familyMemberTravelTypeService, IFamilyMemberTypeService<Domain.Models.FamilyMemberType> familyMemberTypeService) : base(familyMemberTravelTypeService, familyMemberTypeService)
        {
            _familyMemberTravelTypeService = familyMemberTravelTypeService;
            _familyMemberTypeService = familyMemberTypeService;

            RuleFor(familyMember => familyMember.FamilyMemberTypeId).NotEmpty()
                          .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(familyMember => familyMember.LastName).NotEmpty()
                          .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(familyMember => familyMember.Email).NotEmpty()
                          .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(familyMember => familyMember.CreatedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");
        }
    }
}
