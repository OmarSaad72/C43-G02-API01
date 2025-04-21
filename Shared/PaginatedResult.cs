using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record PaginatedResult<T>(int pageSize, int pageIndex, int totalCount,IEnumerable<T> data)
    {
    }
}
