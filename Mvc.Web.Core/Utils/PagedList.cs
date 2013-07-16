using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc.Web.Core.Utils {
    public class PagedList<T> : List<T> {
        private int pageIndex;
        public int PageIndex {
            get { return pageIndex; }
            set {
                if (value < 1) {
                    value = 1;
                } else if (value > PageCount) {
                    value = PageCount;
                }
                pageIndex = value;
            }
        }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage {
            get { return PageIndex > 1; }
        }
        public bool HasNextPage {
            get { return PageIndex < PageCount; }
        }

        /// <summary>
        /// Paging IQueryable with pageindex and pagesize.
        /// </summary>
        /// <param name="items">IQueryable items.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        public PagedList(IQueryable<T> items, int pageIndex, int pageSize) {
            TotalCount = items.Count();
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling((double)TotalCount / PageSize);
            PageIndex = pageIndex;
            if (TotalCount > 0) {
                this.AddRange(items.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList());
            }
        }

        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount) {
            TotalCount = totalCount;
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling((double)TotalCount / PageSize);
            PageIndex = pageIndex;
            this.AddRange(items);
        }
    }
}