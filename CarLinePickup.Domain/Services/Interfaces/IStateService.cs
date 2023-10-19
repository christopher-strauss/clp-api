using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IStateService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<State> GetByAbbreviationAsync(string stateAbbreviation);
        Task<State> GetByNameAsync(string stateName);
    }
}
