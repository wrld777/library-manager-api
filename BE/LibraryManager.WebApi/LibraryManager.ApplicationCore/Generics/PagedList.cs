using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Generics
{
    public class PagedList<T>
    {
        public List<T> Items { get; }
        public int page { get; }
        public int pageSize { get; }
        public int totalCount { get; }
        public bool HasNextPage => page * pageSize > totalCount;
        public bool HasPreviousPage => page > 1;

        public int TotalPages => (int)Math.Ceiling((double)totalCount / pageSize);

        public PagedList(List<T> list, int page, int pageSize, int totalCount)
        {
            Items = list;
            this.page = page;
            this.pageSize = pageSize;
            this.totalCount = totalCount;
        }
    }
}
