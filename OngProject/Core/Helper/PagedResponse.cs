using Microsoft.EntityFrameworkCore;
using OngProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class PagedResponse<T> : ResponseExtension<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }

        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
        public PagedResponse()
        {

        }

        //public static async Task<PagedResponse<T>> Create(IQueryable<T> sourse, int pageNumber, int pageSize)
        //{
        //    var count = await sourse.CountAsync();
        //    var items = await sourse.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        //    return new PagedResponse<T>(items, count, pageNumber, pageSize);
        //}

    }
}
