using System.Threading.Tasks;
using System;
using AutoMapper;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using CarLinePickup.Data.Models;
using CarLinePickup.Options.DomainOptions;
using Arch.EntityFrameworkCore.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using CarLinePickup.Domain.Extensions;
using CarLinePickup.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace CarLinePickup.Domain.Services 
{
    public class VehicleService : ServiceBase<Vehicle, Models.Vehicle>, IVehicleService<Models.Vehicle>
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IVehicleMakeService<Models.VehicleMake> _vehicleMakeService;
        private readonly IVehicleModelService<Models.VehicleModel> _vehicleModelService;
        private readonly IFamilyService<Models.Family> _familyService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public VehicleService(IFamilyService<Models.Family> familyService, IVehicleMakeService<Models.VehicleMake> vehicleMakeService, IVehicleModelService<Models.VehicleModel> vehicleModelService,
            IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _vehicleRepository = unitOfWork.GetRepository<Vehicle>();
            _familyService = familyService;
            _vehicleMakeService = vehicleMakeService;
            _vehicleModelService = vehicleModelService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.Vehicle> CreateAsync(Guid familyId, Models.Vehicle vehicle)
        {
            Vehicle dataVehicle = new Vehicle();

            dataVehicle = _mapper.Map<Vehicle>(vehicle);

            dataVehicle.CreatedDate = DateTime.Now;
            dataVehicle.ModifiedDate = DateTime.Now;

            var family = await _familyService.GetAsync(familyId) ??
                throw new KeyNotFoundException($"There is no {nameof(Family)} with Id: {familyId}");

            var vehicleMake = await _vehicleMakeService.GetAsync(vehicle.MakeId) ??
                throw new KeyNotFoundException($"There is no {nameof(VehicleMake)} with id: {vehicle.MakeId}");

            var vehicleModel = await _vehicleModelService.GetByIdAndYearAsync(vehicle.ModelId, vehicle.Year) ??
                throw new KeyNotFoundException($"There is no {nameof(VehicleModel)} with id: {vehicle.ModelId} and year {vehicle.Year}");

            dataVehicle.FamilyId = familyId;
            dataVehicle.MakeId = vehicleMake.Id;
            dataVehicle.ModelId = vehicleModel.Id;

            //Null out the children so they are not inserted
            dataVehicle.Family = null;
            dataVehicle.Model = null;
            dataVehicle.Make = null;

            await _vehicleRepository.InsertAsync(dataVehicle);

            await _unitOfWork.SaveChangesAsync();

            //get and return the domain object that was created
            return await GetAsync(dataVehicle.Id);
        }
        #endregion

        #region Read
        public async Task<Models.Vehicle> GetAsync(Guid id)
        {
            Func<IQueryable<Vehicle>, IIncludableQueryable<Vehicle, object>> include = source => source.Include(vehicle => vehicle.Model)
                                                                                                        .Include(vehicle => vehicle.Make);
            return await GetAsync(id, _vehicleRepository, include);
        }

        public async Task<Models.PagedResult<Models.Vehicle>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            Func<IQueryable<Vehicle>, IIncludableQueryable<Vehicle, object>> include = source => source.Include(vehicle => vehicle.Model)
                                                                                            .Include(vehicle => vehicle.Make);
            return await GetAllAsync(_vehicleRepository, pageSize, pageIndex, orderBy, include);
        }

        public async Task<Models.PagedResult<Models.VehicleMake>> GetVehicleMakesAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await _vehicleMakeService.GetAllAsync(pageSize, pageIndex, orderBy);
        }

        public async Task<Models.PagedResult<Models.VehicleModel>> GetVehicleModelsByMakeIdAsync(Guid makeId, int pageSize, int pageIndex, string orderBy)
        {
            return await _vehicleModelService.GetAllModelsByMakeIdAsync(makeId, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.PagedResult<Models.VehicleModel>> GetVehicleModelsByMakeIdAndYearAsync(Guid makeId, string year, int pageSize, int pageIndex, string orderBy)
        {
            return await _vehicleModelService.GetAllModelsByMakeIdAndYearAsync(makeId, year, pageSize, pageIndex, orderBy);
        }

        public async Task<IList<string>> GetModelYearsForMakeAsync(Guid makeId) 
        {
            return await _vehicleModelService.GetModelYearsForMakeAsync(makeId);
        }
        #endregion

        #region Update
        public async Task<Models.Vehicle> UpdateAsync(Models.Vehicle domainObject)
        {
            return await UpdateAsync(_vehicleRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await this.DeleteAsync(id, _vehicleRepository);
        }
        #endregion
    }
}

