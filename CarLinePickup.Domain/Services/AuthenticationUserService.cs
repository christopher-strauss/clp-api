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
using CarLinePickup.Domain;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Services
{
    public class AuthenticationUserService : ServiceBase<AuthenticationUser, Models.AuthenticationUser>, IAuthenticationUserService<Models.AuthenticationUser>
    {
        private readonly IRepository<AuthenticationUser> _authenticationUserRepository;
        private readonly IRepository<AuthenticationUserProvider> _authenticationUserProviderRepository;
        private readonly IRepository<AuthenticationUserType> _authenticationUserTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainOptions _options;

        public AuthenticationUserService(IOptions<DomainOptions> options, IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _authenticationUserRepository = unitOfWork.GetRepository<AuthenticationUser>();
            _authenticationUserProviderRepository = unitOfWork.GetRepository<AuthenticationUserProvider>();
            _authenticationUserTypeRepository = unitOfWork.GetRepository<AuthenticationUserType>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _options = options.Value;
        }

        #region Create
        public async Task<Models.AuthenticationUser> CreateAsync(Models.AuthenticationUser authenticationUser)
        {
            AuthenticationUser dataAuthenticationUser = new AuthenticationUser();

            dataAuthenticationUser = _mapper.Map<AuthenticationUser>(authenticationUser);
            
            var dataAuthenticationUserProvider = await _authenticationUserProviderRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == authenticationUser.ProviderType) ??
                throw new KeyNotFoundException($"There is no {nameof(AuthenticationUserProvider)} with name: { authenticationUser.ProviderType }");

            var dataAuthenticationUserType = await _authenticationUserTypeRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == authenticationUser.AuthenticationUserTypeId) ??
                throw new KeyNotFoundException($"There is no {nameof(AuthenticationUserType)} with id: { authenticationUser.AuthenticationUserTypeId }");


            dataAuthenticationUser.AuthenticationUserProviderId = dataAuthenticationUserProvider.Id;
            dataAuthenticationUser.AuthenticationUserTypeId = dataAuthenticationUserType.Id;
            dataAuthenticationUser.CreatedDate = DateTime.Now;

            dataAuthenticationUser.AuthenticationUserProvider = null;
            dataAuthenticationUser.AuthenticationUserType = null;

            await _authenticationUserRepository.InsertAsync(dataAuthenticationUser);

            await _unitOfWork.SaveChangesAsync();

            return await GetAsync(dataAuthenticationUser.Id);
        }
        #endregion

        #region Read
        public async Task<Models.AuthenticationUser> GetByExternalIdAndProviderTypeAsync(string externalProviderId, string providerType)
        {
            Func<IQueryable<AuthenticationUser>, IIncludableQueryable<AuthenticationUser, object>> include = source => source.Include(auth => auth.AuthenticationUserProvider)
                                                                                                                 .Include(auth => auth.AuthenticationUserType);

            var authenticationUser = await _authenticationUserRepository.GetFirstOrDefaultAsync(include: include, predicate: x => x.AuthenticationUserProvider.Name == providerType.ToLower()
                                                                                                        && x.ExternalProviderId == externalProviderId);

            return _mapper.Map<Models.AuthenticationUser>(authenticationUser);
        }

        public async Task<Models.AuthenticationUser> GetAsync(Guid id)
        {
            Func<IQueryable<AuthenticationUser>, IIncludableQueryable<AuthenticationUser, object>> include = source => source.Include(auth => auth.AuthenticationUserProvider)
                                                                                                     .Include(auth => auth.AuthenticationUserType);
                                                                                                     
            return await GetAsync(id, _authenticationUserRepository, include);
        }

        public async Task<Models.PagedResult<Models.AuthenticationUser>> GetAllAsync(int pageSize, int pageIndex, string orderBy)
        {
            Func<IQueryable<AuthenticationUser>, IIncludableQueryable<AuthenticationUser, object>> include = source => source.Include(auth => auth.AuthenticationUserProvider)
                                                                                         .Include(auth => auth.AuthenticationUserType);

            return await GetAllAsync(_authenticationUserRepository, pageSize, pageIndex, orderBy, include);
        }
        #endregion

        #region Update
        public async Task<Models.AuthenticationUser> UpdateAsync(Models.AuthenticationUser domainObject)
        {
            return await UpdateAsync(_authenticationUserRepository, domainObject);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _authenticationUserRepository);
        }
        #endregion
    }
}

