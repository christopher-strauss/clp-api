using System;
using FluentValidation;
using CarLinePickup.API.Models.Request;
using CarLinePickup.Domain.Services.Interfaces;

namespace CarLinePickup.API.Validators.Vehicle
{
    public class VehicleValidatorBase<T> : AbstractValidator<T> where T : VehicleRequestBase
    {
        private readonly IVehicleMakeService<Domain.Models.VehicleMake> _vehicleMakeService;
        private readonly IVehicleModelService<Domain.Models.VehicleModel> _vehicleModelService;

        public VehicleValidatorBase(IVehicleMakeService<Domain.Models.VehicleMake> vehicleMakeService, IVehicleModelService<Domain.Models.VehicleModel> vehicleModelService)
        {
            _vehicleMakeService = vehicleMakeService;
            _vehicleModelService = vehicleModelService;

            RuleFor(vehicle => vehicle.Color)
                                      .MaximumLength(50)
                                      .WithMessage("{PropertyName} can not be null or empty.");
            RuleFor(vehicle => vehicle.LicensePlate)
                                      .MaximumLength(20)
                                      .WithMessage("{PropertyName} can not be null or empty.");
            RuleFor(vehicle => vehicle.Year)
                                      .Length(4)
                                     .WithMessage("{PropertyName} can not be null or empty.");


            When(vehicle => vehicle.MakeId != Guid.Empty, () =>
            {
                RuleFor(vehicle => vehicle.MakeId)
                    .Must((obj, makeId) => (_vehicleMakeService.GetAsync(makeId).Result != null))
                    .WithMessage("Please supply a valid {PropertyName}.");
            });

            When(vehicle => vehicle.ModelId != Guid.Empty, () =>
            {
                RuleFor(vehicle => vehicle.ModelId)
                    .Must((obj, modelId) => (_vehicleModelService.GetAsync(modelId).Result != null))
                    .WithMessage("Please supply a valid {PropertyName}.");

            });


            When(vehicle => (vehicle.ModelId != Guid.Empty || vehicle.MakeId != Guid.Empty || !string.IsNullOrEmpty(vehicle.Year)), () =>
            {
                RuleFor(vehicle => vehicle)
                    .Must((obj) => (_vehicleModelService.GetByMakeIdModelIdAndYearAsync(obj.MakeId, obj.ModelId, obj.Year).Result != null))
                    .WithMessage("Please supply a valid Make, Model and Year combination.");

            });
        }
    }
}
