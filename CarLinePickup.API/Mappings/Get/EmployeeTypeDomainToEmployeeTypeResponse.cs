using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class EmployeeTypeDomainToEmployeeTypeResponse : Profile
    {
        public EmployeeTypeDomainToEmployeeTypeResponse()
        {
            CreateMap<Domain.Models.EmployeeType, EmployeeTypeResponse>();
        }
    }
}
