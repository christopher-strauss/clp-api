using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class EmployeeTypeDataToEmployeeTypeDomain : Profile
    {
        public EmployeeTypeDataToEmployeeTypeDomain()
        {
            CreateMap<Data.Models.EmployeeType, Models.EmployeeType>()
                .ReverseMap();
        }
    }
}

