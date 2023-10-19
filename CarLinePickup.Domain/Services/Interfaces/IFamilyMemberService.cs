using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IFamilyMemberService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> CreateAsync(Guid familyId, FamilyMember familyMember);
        Task<T> GetByEmailAddressAsync(string emailAddress);
        Task<PagedResult<T>> GetAllFamilyMembersByTypeAsync(string familyMemberType, int pageSize, int pageIndex, string orderBy);
        Task<PagedResult<T>> GetAllFamilyMembersByTypeIdAsync(Guid familyMemberTypeId, int pageSize, int pageIndex, string orderBy);
        Task<PagedResult<T>> GetFamilyMembersByFamilyIdAndTypeAsync(Guid familyId, string familyMemberType, int pageSize, int pageIndex, string orderBy);
        Task<PagedResult<T>> GetFamilyMembersByFamilyIdAndTypeIdAsync(Guid familyId, Guid familyMemberTypeId, int pageSize, int pageIndex, string orderBy);
        Task<PagedResult<T>> GetFamilyMembersByFamilyIdAsync(Guid familyId, int pageSize, int pageIndex, string orderBy);
    }
}