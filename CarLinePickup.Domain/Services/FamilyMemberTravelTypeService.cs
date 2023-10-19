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
    public class FamilyMemberTravelTypeService : ServiceBase<FamilyMemberTravelType, Models.FamilyMemberTravelType>, IFamilyMemberTravelTypeService<Models.FamilyMemberTravelType>
    {
        private readonly IRepository<FamilyMemberTravelType> _familyMemberTravelTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public FamilyMemberTravelTypeService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _familyMemberTravelTypeRepository = unitOfWork.GetRepository<FamilyMemberTravelType>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.PagedResult<Models.FamilyMemberTravelType>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_familyMemberTravelTypeRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.FamilyMemberTravelType> GetAsync(Guid id)
        {
            return await GetAsync(id, _familyMemberTravelTypeRepository);
        }

        public async Task<Models.FamilyMemberTravelType> GetByNameAsync(string familyMemberTravelTypeName)
        {
            var dataFamilyMemberTravelType = await _familyMemberTravelTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Description.ToLower() == familyMemberTravelTypeName.ToLower());
            return _mapper.Map<Models.FamilyMemberTravelType>(dataFamilyMemberTravelType);
        }
        #endregion

        #region Update
        public async Task<Models.FamilyMemberTravelType> UpdateAsync(Models.FamilyMemberTravelType domainObject)
        {
            return await UpdateAsync(_familyMemberTravelTypeRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _familyMemberTravelTypeRepository);
        }
        #endregion
    }
}

