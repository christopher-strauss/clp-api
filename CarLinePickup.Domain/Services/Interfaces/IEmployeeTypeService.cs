using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IEmployeeTypeService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> GetByNameAsync(string employeeTypeName);
    }
}
