using System;
using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.API.Validators.Vehicle;
using CarLinePickup.Domain.Services.Interfaces;
using FluentValidation;

namespace CarLinePickup.API.Validators.Grade
{
    public class CreateVehicleValidator : VehicleValidatorBase<VehicleRequestCreate>
    {
        private readonly IVehicleMakeService<Domain.Models.VehicleMake> _vehicleMakeService;
        private readonly IVehicleModelService<Domain.Models.VehicleModel> _vehicleModelService;

        public CreateVehicleValidator(IVehicleMakeService<Domain.Models.VehicleMake> vehicleMakeService, IVehicleModelService<Domain.Models.VehicleModel> vehicleModelService) : base(vehicleMakeService, vehicleModelService)
        {
            _vehicleMakeService = vehicleMakeService;
            _vehicleModelService = vehicleModelService;

            RuleFor(vehicle => vehicle.MakeId).NotEmpty()
                                      .WithMessage("{PropertyName} can not be null or empty.");
            RuleFor(vehicle => vehicle.ModelId).NotEmpty()
                                      .WithMessage("{PropertyName} can not be null or empty.");
            RuleFor(vehicle => vehicle.Color).NotEmpty()
                                      .WithMessage("{PropertyName} can not be null or empty.");
            RuleFor(vehicle => vehicle.LicensePlate).NotEmpty()
                                      .WithMessage("{PropertyName} can not be null or empty.");
            RuleFor(vehicle => vehicle.Year).NotEmpty()
                                      .WithMessage("{PropertyName} can not be null or empty.");

            RuleFor(vehicle => vehicle.CreatedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");
        }
    }
}
