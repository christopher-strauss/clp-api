using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class AddressRequestCreateToAddressDomain : Profile
    {
        public AddressRequestCreateToAddressDomain()
        {
            CreateMap<AddressRequestCreate, Domain.Models.Address>()
                .ReverseMap();
        }
    }
}
