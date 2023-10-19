using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.API.Mappings.Create
{
    public class EmployeeRequestCreateToEmployeeDomain : Profile
    {
        public EmployeeRequestCreateToEmployeeDomain()
        {
            CreateMap<EmployeeRequestCreate, Domain.Models.Employee>()
                .ReverseMap();
        }
    }
}
