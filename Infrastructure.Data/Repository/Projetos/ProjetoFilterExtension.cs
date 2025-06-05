using Domain.Common.Extension;
using Domain.Entities;
using Domain.Filter.Filters.Projetos;
using System.Linq;

namespace Infrastructure.Data.Repository.Projetos
{
    public static class ProjetoFilterExtension
    {
        public static IQueryable<Projeto> WithFilters(this IQueryable<Projeto> queryBase, ProjetosFilter filters)
        {
            var queryFilter = queryBase;

            if (filters.Id.IsSent())
            {
                queryFilter = queryFilter.Where(_ => _.Id == filters.Id);
            }
            if (filters.Nome.IsSent())
            {
                queryFilter = queryFilter.Where(_ => _.Nome.Contains(filters.Nome));
            }
            if (filters.DataCriacaoInicio.IsSent())
            {
                queryFilter = queryFilter.Where(_ => _.DataCriacao >= filters.DataCriacaoInicio);
            }
            if (filters.DataCriacaoFim.IsSent())
            {
                // Considera o fim do dia para DataCriacaoFim
                var dataFim = filters.DataCriacaoFim.Value.AddDays(1).AddMilliseconds(-1);
                queryFilter = queryFilter.Where(_ => _.DataCriacao <= dataFim);
            }

            return queryFilter;
        }

        public static IQueryable<Projeto> OrderByDomain(this IQueryable<Projeto> queryBase, ProjetosFilter filters)
        {
            return queryBase.OrderBy(_ => _.Id);
        }
    }
}
