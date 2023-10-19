using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;
using System.Linq;

namespace CarLinePickup.Domain.Mappings
{
    public class EmployeeDataToEmployeeDomain : Profile
    {
        public EmployeeDataToEmployeeDomain()
        {
            CreateMap<Data.Models.Employee, Models.Employee>()
            .ForMember(dest => dest.EmployeeType, opts => opts.MapFrom(src => src.EmployeeType.Description))
            .ForMember(dest => dest.EmployeeTypeId, opts => opts.MapFrom(src => src.EmployeeType.Id))
            .ForMember(dest => dest.Grades, opt => opt.MapFrom(src => src.EmployeeGrades.Select(eg => eg.Grade)))
            .ReverseMap()
            .ForPath(dest => dest.EmployeeType.Description, opts => opts.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.EmployeeType, opts => opts.Ignore())
            .ForMember(dest => dest.EmployeeGrades, opts => opts.Ignore())
            .ForMember(dest => dest.School, opts => opts.Ignore())
            .ForMember(dest => dest.SchoolId, opts => opts.Ignore());
        }
    }
}

