using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

using CarLinePickup.Domain.Extensions;
using System;

namespace CarLinePickup.API.Mappings.Update
{
    public class EmployeeRequestUpdateToEmployeeDomain : Profile
    {
        public EmployeeRequestUpdateToEmployeeDomain()
        {
            CreateMap<EmployeeRequestUpdate, Domain.Models.Employee>()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
             {
                 if (srcMember != null)
                 {
                     if (srcMember is Guid)
                     {
                         if (((Guid)srcMember) == Guid.Empty)
                         {
                             return false;
                         }
                     }

                     return true;
                 }

                 return false;
              }));
        }
    }
}
