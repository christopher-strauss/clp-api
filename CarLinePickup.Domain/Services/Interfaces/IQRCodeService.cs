using System.Threading.Tasks;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Models.Interfaces;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface IQRCodeService<T> : IDomainService<T> where T : IDomainModel
    {
        Task<T> CreateAsync(T address);
    }
}
