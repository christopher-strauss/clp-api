using CarLinePickup.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface ISchoolService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> CreateAsync(T school);
    }
}
