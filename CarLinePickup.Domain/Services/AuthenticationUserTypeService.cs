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
    public class AuthenticationUserTypeService : ServiceBase<AuthenticationUserType, Models.AuthenticationUserType>, IAuthenticationUserTypeService<Models.AuthenticationUserType>
    {
        private readonly IRepository<AuthenticationUserType> _authenticationUserTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public AuthenticationUserTypeService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _authenticationUserTypeRepository = unitOfWork.GetRepository<AuthenticationUserType>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.PagedResult<Models.AuthenticationUserType>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_authenticationUserTypeRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.AuthenticationUserType> GetAsync(Guid id)
        {
            return await GetAsync(id, _authenticationUserTypeRepository);
        }

        public async Task<Models.AuthenticationUserType> GetByNameAsync(string authenticationUserTypeName)
        {
            var dataAuthenticationUserType = await _authenticationUserTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Type.ToLower() == authenticationUserTypeName.ToLower());
            return _mapper.Map<Models.AuthenticationUserType>(dataAuthenticationUserType);
        }
        #endregion

        #region Update
        public async Task<Models.AuthenticationUserType> UpdateAsync(Models.AuthenticationUserType domainObject)
        {
            return await UpdateAsync(_authenticationUserTypeRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _authenticationUserTypeRepository);
        }
        #endregion
    }
}

