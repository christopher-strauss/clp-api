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
    public class CountyService : ServiceBase<County, Models.County>, ICountyService<Models.County>
    {
        private readonly IRepository<County> _countyRepository;
        private readonly IStateService<Models.State> _stateService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public CountyService(IStateService<Models.State> stateService, IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _countyRepository = unitOfWork.GetRepository<County>();
            _stateService = stateService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.County> GetAsync(Guid id)
        {
            return await GetAsync(id, _countyRepository);
        }

        public async Task<Models.PagedResult<Models.County>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            orderBy = TransformOrderBy(orderBy);

            return await GetAllAsync(_countyRepository, pageSize, pageIndex, orderBy);
        }
        public async Task<Models.PagedResult<Models.County>> GetAllCountiesByStateIdAsync(Guid stateId, int pageSize, int pageIndex, string orderBy)
        {
            
            var state = await _stateService.GetAsync(stateId) ??
                throw new KeyNotFoundException($"There is no {nameof(State)} with Id: {stateId}");

            IList<Models.County> domainCounties = new List<Models.County>();

            orderBy = TransformOrderBy(orderBy);

            var counties = await _countyRepository.GetPagedListAsync(predicate: x => x.StateId == stateId, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy));

            counties.Items.ToList().ForEach(county => domainCounties.Add(_mapper.Map<Models.County>(county)));

            var result = new Models.PagedResult<Models.County>(domainCounties, pageIndex, pageSize, counties.TotalCount, counties.TotalPages);

            return result;
        }

        public async Task<Models.PagedResult<Models.County>> GetAllCountiesByStateAbbreviationAsync(string stateAbbreviation, int pageSize, int pageIndex, string orderBy)
        {
            var state = await _stateService.GetByAbbreviationAsync(stateAbbreviation) ??
                throw new KeyNotFoundException($"There is no {nameof(State)} with abbreviation: {stateAbbreviation}");

            IList<Models.County> domainCounties = new List<Models.County>();

            orderBy = TransformOrderBy(orderBy);

            var counties = await _countyRepository.GetPagedListAsync(predicate: x => x.StateId == state.Id, pageIndex: pageIndex, pageSize: pageSize, orderBy: x => x.OrderBy(orderBy));

            counties.Items.ToList().ForEach(county => domainCounties.Add(_mapper.Map<Models.County>(county)));

            var result = new Models.PagedResult<Models.County>(domainCounties, pageIndex, pageSize, counties.TotalCount, counties.TotalPages);

            return result;
        }

        public async Task<Models.County> GetByNameAsync(string countyName)
        {
            var dataCounty = await _countyRepository.GetFirstOrDefaultAsync(predicate: x => x.CountyName.ToLower() == countyName.ToLower());
            return _mapper.Map<Models.County>(dataCounty);
        }

        public async Task<Models.County> GetByCountyIdAndStateId(Guid countyId, Guid stateId)
        {
            var dataCounty = await _countyRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == countyId && x.StateId == stateId);
            return _mapper.Map<Models.County>(dataCounty);
        }
        #endregion

        #region Update
        public async Task<Models.County> UpdateAsync(Models.County domainObject)
        {
            return await UpdateAsync(_countyRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _countyRepository);
        }
        #endregion

        private string TransformOrderBy(string orderBy)
        {
            //Change the order by to the correct column name
            switch (orderBy.Split(" ")[0].ToLower())
            {
                case "name":
                    orderBy = orderBy.Replace("name", "countyname");
                    break;
                default:
                    break;
            }

            return orderBy;
        }
    }
}

