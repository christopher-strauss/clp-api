using System.Threading.Tasks;

namespace CarLinePickup.Domain.Services.Interfaces
{
    public interface ITileService
    {
        Task<bool> AlertAsync(int id);
    }
}

