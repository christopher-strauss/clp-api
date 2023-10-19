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
    public class AuthenticationUserProviderService : ServiceBase<AuthenticationUserProvider, Models.AuthenticationUserProvider>, IAuthenticationUserProviderService<Models.AuthenticationUserProvider>
    {
        private readonly IRepository<AuthenticationUserProvider> _authenticationUserProviderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public AuthenticationUserProviderService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _authenticationUserProviderRepository = unitOfWork.GetRepository<AuthenticationUserProvider>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        #endregion

        #region Read
        public async Task<Models.PagedResult<Models.AuthenticationUserProvider>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            return await GetAllAsync(_authenticationUserProviderRepository, pageSize, pageIndex, orderBy);
        }

        public async Task<Models.AuthenticationUserProvider> GetAsync(Guid id)
        {
            return await GetAsync(id, _authenticationUserProviderRepository);
        }

        public async Task<Models.AuthenticationUserProvider> GetByNameAsync(string authenticationUserProviderName)
        {
            var dataAuthenticationUserProvider = await _authenticationUserProviderRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == authenticationUserProviderName.ToLower());
            return _mapper.Map<Models.AuthenticationUserProvider>(dataAuthenticationUserProvider);
        }
        #endregion

        #region Update
        public async Task<Models.AuthenticationUserProvider> UpdateAsync(Models.AuthenticationUserProvider domainObject)
        {
            return await UpdateAsync(_authenticationUserProviderRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _authenticationUserProviderRepository);
        }
        #endregion
    }
}

