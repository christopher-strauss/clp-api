using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IVehicleService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> CreateAsync(Guid familyId, T vehicle);
        Task<PagedResult<VehicleMake>> GetVehicleMakesAsync(int pageSize, int pageIndex, string orderBy);
        Task<PagedResult<VehicleModel>> GetVehicleModelsByMakeIdAsync(Guid makeId, int pageSize, int pageIndex, string orderBy);
        Task<PagedResult<VehicleModel>> GetVehicleModelsByMakeIdAndYearAsync(Guid makeId, string year, int pageSize, int pageIndex, string orderBy);
        Task<IList<string>> GetModelYearsForMakeAsync(Guid makeId);
    }
}
