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
using CarLinePickup.Domain.Extensions;
using CarLinePickup.Data.Models;
using CarLinePickup.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace CarLinePickup.Domain.Services
{
    public class FamilyService : ServiceBase<Family, Models.Family>, IFamilyService<Models.Family>
    {
        private readonly IRepository<Family> _familyRepository;
        private readonly ICountyService<Models.County> _countyService;
        private readonly IStateService<Models.State> _stateService;
        private readonly IRepository<AuthenticationUserProvider> _authenticationUserProviderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public FamilyService(IStateService<Models.State> stateService, ICountyService<Models.County> countyService, IAddressService<Models.Address> addressService, IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _familyRepository = unitOfWork.GetRepository<Family>();
            _authenticationUserProviderRepository = unitOfWork.GetRepository<AuthenticationUserProvider>();
            _countyService = countyService;
            _stateService = stateService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.Family> CreateAsync(Models.Family family)
        {
            Family dataFamily = new Family();
            Address dataAddress = new Address();

            dataFamily = _mapper.Map<Family>(family);

            dataFamily.CreatedDate = DateTime.Now;
            dataFamily.ModifiedDate = DateTime.Now;

            var county = await _countyService.GetAsync(family.Address.CountyId) ??
                  throw new KeyNotFoundException($"There is no {nameof(County)} with id: {family.Address.CountyId}");

            var state = await _stateService.GetAsync(family.Address.StateId) ??
                  throw new KeyNotFoundException($"There is no {nameof(State)} with id: {family.Address.StateId}");

            dataFamily.Address = _mapper.Map<Address>(family.Address);

            dataFamily.Address.CreatedDate = DateTime.Now;
            dataFamily.Address.ModifiedDate = DateTime.Now;
            dataFamily.Address.StateId = state.Id;
            dataFamily.Address.CountyId = county.Id;

            await _familyRepository.InsertAsync(dataFamily);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Models.Family>(dataFamily);
        }
        public async Task<Models.FamilyRegistration> RegisterAsync(Models.FamilyRegistration familyRegistration)
        {
            var authenticationUser = familyRegistration.FamilyMembers.FirstOrDefault().AuthenticationUser;

            var dataFamily = _mapper.Map<Family>(familyRegistration);

            dataFamily.CreatedDate = DateTime.Now;
            dataFamily.Address.CreatedDate = DateTime.Now;

            var dataAuthenticationUserProvider = await _authenticationUserProviderRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == authenticationUser.ProviderType) ??
                throw new KeyNotFoundException($"There is no {nameof(AuthenticationUserProvider)} with name: {authenticationUser.ProviderType}");

            dataFamily.FamilyMembers.FirstOrDefault().AuthenticationUser.AuthenticationUserProviderId = dataAuthenticationUserProvider.Id;
            
            await _familyRepository.InsertAsync(dataFamily);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Models.FamilyRegistration>(dataFamily);
        }
        #endregion

        #region Read
        public async Task<Models.Family> GetAsync(Guid id)
        {
            Func<IQueryable<Family>, IIncludableQueryable<Family, object>> include = source => source.Include(family => family.FamilyMembers)
                                                                                                                    .ThenInclude(fm => fm.FamilyMemberType)
                                                                                                       .Include(family => family.FamilyMembers)
                                                                                                                    .ThenInclude(fm => fm.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Grade)
                                                                                                       .Include(family => family.FamilyMembers)
                                                                                                                    .ThenInclude(fm => fm.EmployeeGrade)
                                                                                                                    .ThenInclude(eg => eg.Employee)
                                                                                                                    .ThenInclude(e => e.School)
                                                                                                        .Include(family => family.Vehicles)
                                                                                                                    .ThenInclude(vehicle => vehicle.Make)
                                                                                                        .Include(family => family.Vehicles)
                                                                                                                    .ThenInclude(vehicle => vehicle.Model)
                                                                                                        .Include(family => family.Address)
                                                                                                                    .ThenInclude(address => address.State)
                                                                                                        .Include(family => family.Address)
                                                                                                                    .ThenInclude(address => address.County);


            return await GetAsync(id, _familyRepository, include);
        }

        public async Task<Models.PagedResult<Models.Family>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            Func<IQueryable<Family>, IIncludableQueryable<Family, object>> include = source => source.Include(family => family.FamilyMembers)
                                                                                                        .ThenInclude(fm => fm.FamilyMemberType)
                                                                                            .Include(family => family.FamilyMembers)
                                                                                                        .ThenInclude(fm => fm.EmployeeGrade)
                                                                                                        .ThenInclude(eg => eg.Grade)
                                                                                            .Include(family => family.FamilyMembers)
                                                                                                        .ThenInclude(fm => fm.EmployeeGrade)
                                                                                                        .ThenInclude(eg => eg.Employee)
                                                                                                        .ThenInclude(e => e.School)
                                                                                           .Include(family => family.Vehicles)
                                                                                                        .ThenInclude(vehicle => vehicle.Make)
                                                                                           .Include(family => family.Vehicles)
                                                                                                        .ThenInclude(vehicle => vehicle.Model)
                                                                                           .Include(family => family.Address)
                                                                                                        .ThenInclude(address => address.State)
                                                                                            .Include(family => family.Address)
                                                                                                        .ThenInclude(address => address.County);

            return await GetAllAsync(_familyRepository, pageSize, pageIndex, orderBy, include);
        }
        #endregion

        #region Update
        public async Task<Models.Family> UpdateAsync(Models.Family domainObject)
        {
            return await UpdateAsync(_familyRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _familyRepository);
        }
        #endregion
    }
}

