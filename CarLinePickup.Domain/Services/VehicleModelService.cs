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

namespace CarLinePickup.Domain.Services 
{
    public class VehicleModelService : ServiceBase<VehicleModel, Models.VehicleModel>, IVehicleModelService<Models.VehicleModel>
    {
        private readonly IRepository<VehicleModel> _vehicleModelRepository;
        private readonly IVehicleMakeService<Models.VehicleMake> _vehicleMakeService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public VehicleModelService(IVehicleMakeService<Models.VehicleMake> vehicleMakeService, IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _vehicleModelRepository = unitOfWork.GetRepository<VehicleModel>();
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.PagedResult<Models.VehicleModel>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_vehicleModelRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.VehicleModel> GetAsync(Guid id)
        {
            return await GetAsync(id, _vehicleModelRepository);
        }
        #endregion

        #region Update
        public async Task<Models.VehicleModel> UpdateAsync(Models.VehicleModel domainObject)
        {
            return await UpdateAsync(_vehicleModelRepository, domainObject);
        }

        public async Task<Models.PagedResult<Models.VehicleModel>> GetAllModelsByMakeIdAsync(Guid makeId, int pageSize, int pageIndex, string orderBy)
        {

            var vehicleMake = await _vehicleMakeService.GetAsync(makeId) ??
                throw new KeyNotFoundException($"There is no {nameof(VehicleMake)} with Id: {makeId}");

            IList<Models.VehicleModel> domainVehicleModels = new List<Models.VehicleModel>();

            var vehicleModels = await _vehicleModelRepository.GetPagedListAsync(predicate: x => x.VehicleMakeId == makeId, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy));

            vehicleModels.Items.ToList().ForEach(vehicleModel => domainVehicleModels.Add(_mapper.Map<Models.VehicleModel>(vehicleModel)));

            var result = new Models.PagedResult<Models.VehicleModel>(domainVehicleModels, pageIndex, pageSize, vehicleModels.TotalCount, vehicleModels.TotalPages);

            return result;
        }

        public async Task<Models.PagedResult<Models.VehicleModel>> GetAllModelsByMakeIdAndYearAsync(Guid makeId, string year, int pageSize, int pageIndex, string orderBy)
        {

            var vehicleMake = await _vehicleMakeService.GetAsync(makeId) ??
                throw new KeyNotFoundException($"There is no {nameof(VehicleMake)} with Id: {makeId}");

            IList<Models.VehicleModel> domainVehicleModels = new List<Models.VehicleModel>();

            var vehicleModels = await _vehicleModelRepository.GetPagedListAsync(predicate: x => x.VehicleMakeId == makeId && x.Year == year, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy));

            vehicleModels.Items.ToList().ForEach(vehicleModel => domainVehicleModels.Add(_mapper.Map<Models.VehicleModel>(vehicleModel)));

            var result = new Models.PagedResult<Models.VehicleModel>(domainVehicleModels, pageIndex, pageSize, vehicleModels.TotalCount, vehicleModels.TotalPages);

            return result;
        }

        public async Task<IList<string>> GetModelYearsForMakeAsync(Guid makeId)
        {

            var vehicleMake = await _vehicleMakeService.GetAsync(makeId) ??
                throw new KeyNotFoundException($"There is no {nameof(VehicleMake)} with Id: {makeId}");

            IList<string> years = new List<string>();

            var vehicleModels = await _vehicleModelRepository.GetPagedListAsync(predicate: x => x.VehicleMakeId == makeId);


            var groupedYears = vehicleModels.Items.GroupBy(vehicleModel => vehicleModel.Year).Select(grp => grp.First()).OrderBy(x => x.Year);

            groupedYears.ToList().ForEach(vehicleModel => years.Add(vehicleModel.Year));

            return years;
        }

        public async Task<Models.VehicleModel> GetByNameAsync(string vehicleModelName)
        {
            var dataVehicleModel = await _vehicleModelRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == vehicleModelName.ToLower());
            return _mapper.Map<Models.VehicleModel>(dataVehicleModel);
        }

        public async Task<Models.VehicleModel> GetByIdAndYearAsync(Guid id, string year)
        {
            var dataVehicleModel = await _vehicleModelRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == id && x.Year == year);
            return _mapper.Map<Models.VehicleModel>(dataVehicleModel);
        }

        public async Task<Models.VehicleModel> GetByMakeIdModelIdAndYearAsync(Guid makeId, Guid modelId, string year)
        {
            var dataVehicleModel = await _vehicleModelRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == modelId && x.VehicleMakeId == makeId && x.Year == year);
            return _mapper.Map<Models.VehicleModel>(dataVehicleModel);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _vehicleModelRepository);
        }
        #endregion
    }
}

