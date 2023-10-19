using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IVehicleModelService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<PagedResult<T>> GetAllModelsByMakeIdAsync(Guid makeId, int pageSize, int pageIndex, string orderBy);
        Task<PagedResult<T>> GetAllModelsByMakeIdAndYearAsync(Guid makeId, string year, int pageSize, int pageIndex, string orderBy);
        Task<IList<string>> GetModelYearsForMakeAsync(Guid makeId);
        Task<T> GetByNameAsync(string vehicleModelName);
        Task<T> GetByIdAndYearAsync(Guid id, string year);
        Task<T> GetByMakeIdModelIdAndYearAsync(Guid makeId, Guid modelId, string year);
    }
}
