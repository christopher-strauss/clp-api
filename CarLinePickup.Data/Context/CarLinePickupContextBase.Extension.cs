using CarLinePickup.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarLinePickup.Data.Context
{
    public partial class CarLinePickupContextBase : IDbContext
    {
        //Needed to add this constructor.  When calling services.AddDbContext and having a base context the extension method 
        //is expecting a contstructor of DbContextOptions<DerievedConxtextType> when regestering the context with the DI container. 
        public CarLinePickupContextBase(DbContextOptions options) : base(options)
        {
        }
    }
}
