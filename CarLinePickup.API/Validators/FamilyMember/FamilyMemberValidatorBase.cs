using System;
using FluentValidation;
using CarLinePickup.API.Models.Request;
using CarLinePickup.Domain.Services.Interfaces;


namespace CarLinePickup.API.Validators.FamilyMember
{
    public class FamilyMemberValidatorBase<T> : AbstractValidator<T> where T : FamilyMemberRequestBase
    {
        private readonly IFamilyMemberTravelTypeService<Domain.Models.FamilyMemberTravelType> _familyMemberTravelTypeService;
        private readonly IFamilyMemberTypeService<Domain.Models.FamilyMemberType> _familyMemberTypeService;

        public FamilyMemberValidatorBase(IFamilyMemberTravelTypeService<Domain.Models.FamilyMemberTravelType> familyMemberTravelTypeService, IFamilyMemberTypeService<Domain.Models.FamilyMemberType> familyMemberTypeService)
        {
            _familyMemberTravelTypeService = familyMemberTravelTypeService;
            _familyMemberTypeService = familyMemberTypeService;

            //TODO:  Need to fix
            //When(familyMember => familyMember.FamilyMemberTravelTypeId != Guid.Empty && familyMember.FamilyMemberTravelTypeId != null, () =>
            //{
            //    RuleFor(familyMember => familyMember)
            //        .Must((obj) => _familyMemberTypeService.GetAsync(obj.FamilyMemberTypeId).Result.Description.ToLower() == "parent")
            //        .WithMessage("A family member of type parent can not have a travel type.");

            //    RuleFor(familyMember => familyMember.FamilyMemberTravelTypeId)
            //        .Must((obj, familyMemberTravelTypeId) => (_familyMemberTravelTypeService.GetAsync(familyMemberTravelTypeId.Value).Result != null))
            //        .WithMessage("Please supply a valid {PropertyName}.");
            //});

            When(familyMember => familyMember.FamilyMemberTypeId != Guid.Empty, () =>
            {
                RuleFor(familyMember => familyMember.FamilyMemberTypeId)
                    .Must((obj, familyMemberTypeId) => (_familyMemberTypeService.GetAsync(familyMemberTypeId).Result != null))
                    .WithMessage("Please supply a valid {PropertyName}.");
            });

            RuleFor(familyMember => familyMember.FirstName)
                          .MaximumLength(100)
                          .WithMessage("{PropertyName} can not be greater than 100 characters.");

            RuleFor(familyMember => familyMember.LastName)
                          .MaximumLength(100)
                          .WithMessage("{PropertyName} can not be greater than 100 characters.");

            RuleFor(familyMember => familyMember.Email)
              .MaximumLength(50)
              .WithMessage("{PropertyName} can not be greater than 50 characters.");

            RuleFor(familyMember => familyMember.Email).EmailAddress()
              .WithMessage("Please provide a valid email address.");

            RuleFor(familyMember => familyMember.Phone).Matches("^[0-9]{3}-[0-9]{3}-[0-9]{4}$")
                .WithMessage("Please enter the phone number in xxx-xxx-xxxx format.");         
        }
    }
}
