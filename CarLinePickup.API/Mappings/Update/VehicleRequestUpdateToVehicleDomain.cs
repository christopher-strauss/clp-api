using AutoMapper;
using CarLinePickup.API.Models.Request.Update;
using System;

namespace CarLinePickup.API.Mappings.Update
{
    public class VehicleRequestUpdateToVehicleDomain : Profile
    {
        public VehicleRequestUpdateToVehicleDomain()
        {
            CreateMap<VehicleRequestUpdate, Domain.Models.Vehicle>()
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
