using System;
using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IEmployeeService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> GetByEmailAddressAsync(string emailAddress);
        Task<T> CreateAsync(Guid schoolId, T employee);
        Task<Models.PagedResult<T>> GetEmployeesBySchoolId(Guid schoolId, int pageSize, int pageIndex, string orderBy);
        Task<T> UpdateStatus(Models.Employee domainObject, string status);
    }
}
