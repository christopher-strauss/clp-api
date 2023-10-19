using CarLinePickup.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IDomainService<T>
    {
        Task<T> GetAsync(Guid id);
        Task<PagedResult<T>> GetAllAsync(int pageSize, int pageIndex, string orderBy);
        Task DeleteAsync(Guid id);
        Task<T> UpdateAsync(T domainObject);
    }
}
