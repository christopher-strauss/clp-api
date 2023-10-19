using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class AddressDomainToAddressResponse : Profile
    {
        public AddressDomainToAddressResponse()
        {
            CreateMap<Domain.Models.Address, AddressResponse>();
        }
    }
}
