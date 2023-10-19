using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class EmployeeDomainToEmployeeResponse : Profile
    {
        public EmployeeDomainToEmployeeResponse()
        {
            CreateMap<Domain.Models.Employee, EmployeeResponse>();
        }
    }
}
