using System;
using System.Threading.Tasks;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IAuthenticationUserService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> CreateAsync(T authenticationUser);
        Task<T> GetByExternalIdAndProviderTypeAsync(string externalProviderId, string providerType);
    }
}
