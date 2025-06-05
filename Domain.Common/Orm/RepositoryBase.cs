using Domain.Common.Filters;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Orm
{
    public class RepositoryBase
    {
        public async Task<int> CountAsync<T2>(IQueryable<T2> source)
        {
            return await source.CountAsync();
        }

        public Task<List<T2>> ToListAsync<T2>(IQueryable<T2> source)
        {
            return source.ToListAsync();
        }


        public IQueryable<T2> Paging<T2>(IQueryable<T2> source, FilterBase filter, int totalCount)
        {
            if (filter.IsPagination)
            {
                var pageIndex = filter.PageIndex > 0 ? filter.PageIndex - 1 : 0;
                var pageSize = filter.PageSize;
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                return source.Skip(filter.PageSkipped).Take(pageSize);
            }

            return source;

        }
    }
}
