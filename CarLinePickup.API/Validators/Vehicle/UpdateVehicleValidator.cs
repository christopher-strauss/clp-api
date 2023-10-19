using FluentValidation;
using CarLinePickup.API.Models.Request.Update;
using CarLinePickup.Domain.Services.Interfaces;

namespace CarLinePickup.API.Validators.Vehicle
{
    public class UpdateVehicleValidator : VehicleValidatorBase<VehicleRequestUpdate>
    {
        private readonly IVehicleMakeService<Domain.Models.VehicleMake> _vehicleMakeService;
        private readonly IVehicleModelService<Domain.Models.VehicleModel> _vehicleModelService;

        public UpdateVehicleValidator(IVehicleMakeService<Domain.Models.VehicleMake> vehicleMakeService, IVehicleModelService<Domain.Models.VehicleModel> vehicleModelService) : base(vehicleMakeService, vehicleModelService)
        {
            _vehicleMakeService = vehicleMakeService;
            _vehicleModelService = vehicleModelService;

            RuleFor(vehicle => vehicle.ModifiedBy)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} can not be null, empty or greater than 50 characters.");
        }
    }
}
