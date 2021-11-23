using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperAdvanced.Models
{
    public class PagedList<T>

    {
        public List<T> Items { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages => (int)Math.Ceiling((float)TotalRecords / PageSize);
        public bool HasNextPages => PageNumber < TotalPages;
        public bool HasPreviousPages => PageNumber > 1;

        public PagedList(List<T> Items,int PageSize,int PageNumber, int TotalRecords)
            {
            this.Items = Items;
            this.PageSize = PageSize;
            this.PageNumber = PageNumber;
            this.TotalRecords = TotalRecords;

            }

     
    }
}