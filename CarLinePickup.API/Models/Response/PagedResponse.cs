using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class PagedResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public IList<T> Data { get; set; }
    }
}
