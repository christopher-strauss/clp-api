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
    public class VehicleMakeService : ServiceBase<VehicleMake, Models.VehicleMake>, IVehicleMakeService<Models.VehicleMake>
    {
        private readonly IRepository<VehicleMake> _vehicleMakeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public VehicleMakeService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _vehicleMakeRepository = unitOfWork.GetRepository<VehicleMake>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.PagedResult<Models.VehicleMake>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_vehicleMakeRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.VehicleMake> GetAsync(Guid id)
        {
            return await GetAsync(id, _vehicleMakeRepository);
        }

        public async Task<Models.VehicleMake> GetByNameAsync(string vehicleMakeName)
        {
            var dataVehicleMake = await _vehicleMakeRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == vehicleMakeName.ToLower());
            return _mapper.Map<Models.VehicleMake>(dataVehicleMake);
        }
        #endregion

        #region Update
        public async Task<Models.VehicleMake> UpdateAsync(Models.VehicleMake domainObject)
        {
            return await UpdateAsync(_vehicleMakeRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _vehicleMakeRepository);
        }
        #endregion
    }
}

