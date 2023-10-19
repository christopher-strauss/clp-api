using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IFamilyService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> CreateAsync(Family family);
        Task<FamilyRegistration> RegisterAsync(FamilyRegistration familyRegistration);
    }
}
    