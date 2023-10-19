using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarLinePickup.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using CarLinePickup.Options.DomainOptions;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CarLinePickup.Data.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace CarLinePickup.Domain.Services
{
    public class AddressService : ServiceBase<Address, Models.Address>, IAddressService<Models.Address>
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IStateService<Models.State> _stateService;
        private readonly ICountyService<Models.County> _countyService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public AddressService(IStateService<Models.State> stateService, ICountyService<Models.County> countyService, IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _addressRepository = unitOfWork.GetRepository<Address>();
            _countyService = countyService;
            _stateService = stateService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.Address> CreateAsync(Models.Address address)
        {
            Address dataAddress = new Address();

            dataAddress = _mapper.Map<Address>(address);

            dataAddress.CreatedDate = DateTime.Now;
            dataAddress.ModifiedDate = DateTime.Now;

            var county = await _countyService.GetByNameAsync(address.County) ??
                  throw new KeyNotFoundException($"There is no {nameof(County)} with Name: {address.County}");

            var state = await _stateService.GetByNameAsync(address.State) ??
                  throw new KeyNotFoundException($"There is no {nameof(State)} with Name: {address.State}");

            dataAddress.StateId = state.Id;
            dataAddress.CountyId = county.Id;

            dataAddress.County = null;
            dataAddress.State = null;

            await _addressRepository.InsertAsync(dataAddress);

            await _unitOfWork.SaveChangesAsync();

            return await GetAsync(dataAddress.Id);
        }
        #endregion

        #region Read
        public async Task<Models.Address> GetAsync(Guid id)
        {
            Func<IQueryable<Address>, IIncludableQueryable<Address, object>> include = source => source.Include(address => address.County)
                                                                                           .Include(familyMember => familyMember.State);

            return await GetAsync(id, _addressRepository, include);
        }

        public async Task<Models.PagedResult<Models.Address>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            Func<IQueryable<Address>, IIncludableQueryable<Address, object>> include = source => source.Include(address => address.County)
                                                                                                       .Include(address => address.State);

            return await GetAllAsync(_addressRepository, pageSize, pageIndex, orderBy, include);
        }
        #endregion

        #region Update
        public async Task<Models.Address> UpdateAsync(Models.Address domainObject)
        {
            return await UpdateAsync(_addressRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _addressRepository);
        }
        #endregion
    }
}

