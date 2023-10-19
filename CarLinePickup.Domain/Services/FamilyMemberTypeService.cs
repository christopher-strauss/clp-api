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
    public class FamilyMemberTypeService : ServiceBase<FamilyMemberType, Models.FamilyMemberType>, IFamilyMemberTypeService<Models.FamilyMemberType>
    {
        private readonly IRepository<FamilyMemberType> _familyMemberTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public FamilyMemberTypeService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _familyMemberTypeRepository = unitOfWork.GetRepository<FamilyMemberType>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.PagedResult<Models.FamilyMemberType>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_familyMemberTypeRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.FamilyMemberType> GetAsync(Guid id)
        {
            return await GetAsync(id, _familyMemberTypeRepository);
        }

        public async Task<Models.FamilyMemberType> GetByNameAsync(string familyMemberTypeName)
        {
            var dataFamilyMemberType = await _familyMemberTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Description.ToLower() == familyMemberTypeName.ToLower());
            return _mapper.Map<Models.FamilyMemberType>(dataFamilyMemberType);
        }
        #endregion

        #region Update
        public async Task<Models.FamilyMemberType> UpdateAsync(Models.FamilyMemberType domainObject)
        {
            return await UpdateAsync(_familyMemberTypeRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _familyMemberTypeRepository);
        }
        #endregion
    }
}

