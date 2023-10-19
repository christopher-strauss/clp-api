using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface ICountyService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<PagedResult<T>> GetAllCountiesByStateIdAsync(Guid stateId, int pageSize, int pageIndex, string orderBy);
        Task<T> GetByNameAsync(string countyName);
        Task<PagedResult<T>> GetAllCountiesByStateAbbreviationAsync(string stateAbbreviation, int pageSize, int pageIndex, string orderBy);
        Task<T> GetByCountyIdAndStateId(Guid countyId, Guid stateId);
    }
}
