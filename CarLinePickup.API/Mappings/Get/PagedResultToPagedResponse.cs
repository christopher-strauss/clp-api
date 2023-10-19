using System;
using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class PagedResultToPagedResponse : Profile
    {
        public PagedResultToPagedResponse()
        {
            CreateMap(typeof(Domain.Models.PagedResult<>), typeof(PagedResponse<>));
        }
    }
}
