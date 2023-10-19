using FluentValidation;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.API.Validators.Extensions;

namespace CarLinePickup.API.Validators.FamilyMember
{
    public class UpdateFamilyMemberValidator : FamilyMemberValidatorBase<FamilyMemberRequestUpdate>
    {
        private readonly IFamilyMemberTravelTypeService<Domain.Models.FamilyMemberTravelType> _familyMemberTravelTypeService;
        private readonly IFamilyMemberTypeService<Domain.Models.FamilyMemberType> _familyMemberTypeService;

        public UpdateFamilyMemberValidator(IFamilyMemberTravelTypeService<Domain.Models.FamilyMemberTravelType> familyMemberTravelTypeService, IFamilyMemberTypeService<Domain.Models.FamilyMemberType> familyMemberTypeService) : base(familyMemberTravelTypeService, familyMemberTypeService)
        {
            _familyMemberTravelTypeService = familyMemberTravelTypeService;
            _familyMemberTypeService = familyMemberTypeService;

            RuleFor(familyMember => familyMember.ModifiedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be greater than 50 characters.");
        }
    }
}
