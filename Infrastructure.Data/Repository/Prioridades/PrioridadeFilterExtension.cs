using Domain.Entities;
using Domain.Filter.Filters.Prioridades;
using System.Linq;

namespace Infrastructure.Data.Repository.Prioridades
{
    public static class PrioridadeFilterExtension
    {
        public static IQueryable<Prioridade> WithFilters(this IQueryable<Prioridade> queryBase, PrioridadeFilter filters)
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

        public static IQueryable<Prioridade> OrderByDomain(this IQueryable<Prioridade> queryBase, PrioridadeFilter filters)
        {
            return queryBase.OrderBy(_ => _.Id);
        }
    }
}
