using Domain.Entities;
using Domain.Filter.Filters.StatusTarefa;
using System.Linq;

namespace Infrastructure.Data.Repository.StatusTarefas
{
    public static class StatusTarefaFilterExtension
    {
        public static IQueryable<StatusTarefa> WithFilters(this IQueryable<StatusTarefa> queryBase, StatusTarefaFilter filters)
        {
            var queryFilter = queryBase;

            if (filters.Id.HasValue)
            {
                queryFilter = queryFilter.Where(_ => _.Id == filters.Id);
            }
            if (!string.IsNullOrEmpty(filters.Nome))
            {
                queryFilter = queryFilter.Where(_ => _.Nome.Contains(filters.Nome));
            }

            return queryFilter;
        }

        public static IQueryable<StatusTarefa> OrderByDomain(this IQueryable<StatusTarefa> queryBase, StatusTarefaFilter filters)
        {
            return queryBase.OrderBy(_ => _.Id);
        }
    }
}
