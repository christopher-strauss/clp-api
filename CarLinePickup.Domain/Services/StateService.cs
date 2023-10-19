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
    public class StateService : ServiceBase<State, Models.State>, IStateService<Models.State>
    {
        private readonly IRepository<State> _stateRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public StateService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _stateRepository = unitOfWork.GetRepository<State>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.State> GetAsync(Guid id)
        {
            return await GetAsync(id, _stateRepository);
        }

        public async Task<Models.PagedResult<Models.State>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            orderBy = TransformOrderBy(orderBy);

            return await GetAllAsync(_stateRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.State> GetByNameAsync(string stateName)
        {
            var dataState = await _stateRepository.GetFirstOrDefaultAsync(predicate: x => x.StateName.ToLower() == stateName.ToLower());
            return _mapper.Map<Models.State>(dataState);
        }

        public async Task<Models.State> GetByAbbreviationAsync(string stateAbbreviation)
        {
            var dataState = await _stateRepository.GetFirstOrDefaultAsync(predicate: x => x.StateAbbreviation.ToLower() == stateAbbreviation.ToLower());
            return _mapper.Map<Models.State>(dataState);
        }
        #endregion

        #region Update
        public async Task<Models.State> UpdateAsync(Models.State domainObject)
        {
            return await UpdateAsync(_stateRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _stateRepository);
        }
        #endregion

        private string TransformOrderBy(string orderBy)
        {
            //Change the order by to the correct column name
            switch (orderBy.Split(" ")[0].ToLower())
            {
                case "name":
                    orderBy = orderBy.Replace("name", "statename");
                    break;
                case "abbreviation":
                    orderBy = orderBy.Replace("abbreviation", "stateabbreviation");
                    break;
                default:
                    break;
            }

            return orderBy;
        }
    }
}

