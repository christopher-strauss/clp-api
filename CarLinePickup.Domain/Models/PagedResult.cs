using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class PagedResult<T>
    {
        public PagedResult(IList<T> data, int pageIndex, int pageSize, int totalRecords, int totalPages)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = totalPages;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public IList<T> Data { get; set; }
    }
}
