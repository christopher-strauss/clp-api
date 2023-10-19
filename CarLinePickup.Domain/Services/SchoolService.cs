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
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace CarLinePickup.Domain.Services
{
    public class SchoolService : ServiceBase<School, Models.School>, ISchoolService<Models.School>
    {
        private readonly IRepository<School> _schoolRepository;
        private readonly ICountyService<Models.County> _countyService;
        private readonly IStateService<Models.State> _stateService;
        private readonly IAddressService<Models.Address> _addressService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public SchoolService(IStateService<Models.State> stateService, ICountyService<Models.County> countyService, IAddressService<Models.Address> addressService, IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _schoolRepository = unitOfWork.GetRepository<School>();
            _countyService = countyService;
            _stateService = stateService;
            _addressService = addressService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.School> CreateAsync(Models.School school)
        {
            School dataSchool = new School();

            dataSchool = _mapper.Map<School>(school);

            dataSchool.CreatedDate = DateTime.Now;
            dataSchool.ModifiedDate = DateTime.Now;

            var county = await _countyService.GetByNameAsync(school.Address.County) ??
                  throw new KeyNotFoundException($"There is no {nameof(County)} with Name: {school.Address.County}");

            var state = await _stateService.GetByNameAsync(school.Address.State) ??
                  throw new KeyNotFoundException($"There is no {nameof(State)} with Name: {school.Address.State}");

            dataSchool.Address = _mapper.Map<Address>(school.Address);
            dataSchool.Address.CreatedDate = DateTime.Now;
            dataSchool.Address.ModifiedDate = DateTime.Now;
            dataSchool.Address.StateId = state.Id;
            dataSchool.Address.CountyId = county.Id;

            await _schoolRepository.InsertAsync(dataSchool);

            await _unitOfWork.SaveChangesAsync();
          
            return await GetAsync(dataSchool.Id);
        }
        #endregion

        #region Read
        public async Task<Models.School> GetAsync(Guid id)
        {
            Func<IQueryable<School>, IIncludableQueryable<School, object>> include = source => source.Include(school => school.Address)
                                                                                                        .ThenInclude(a => a.County)
                                                                                           .Include(school => school.Address)
                                                                                                        .ThenInclude(a => a.State)
                                                                                           .Include(school => school.Grades);
            return await GetAsync(id, _schoolRepository, include);
        }

        public async Task<Models.PagedResult<Models.School>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            Func<IQueryable<School>, IIncludableQueryable<School, object>> include = source => source.Include(school => school.Address)
                                                                                            .ThenInclude(a => a.County)
                                                                               .Include(school => school.Address)
                                                                                            .ThenInclude(a => a.State)
                                                                               .Include(school => school.Grades);
            orderBy = TransformOrderBy(orderBy);

            return await GetAllAsync(_schoolRepository, pageSize, pageIndex, orderBy, include);
        }
        #endregion

        #region Update
        public async Task<Models.School> UpdateAsync(Models.School domainObject)
        {
            return await UpdateAsync(_schoolRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _schoolRepository);
        }
        #endregion
        private string TransformOrderBy(string orderBy)
        {
            //Change the order by to the correct column name
            switch (orderBy.Split(" ")[0].ToLower())
            {
                case "name":
                    orderBy = orderBy.Replace("name", "schoolname");
                    break;
                default:
                    break;
            }

            return orderBy;
        }
    }
}

