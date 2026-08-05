using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Common
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(IReadOnlyList<TEntity> data, int pageIndex, int pageSize, int count)
        {
            this.data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
        }

        public IReadOnlyList<TEntity> data { get; set; } = [];
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}
