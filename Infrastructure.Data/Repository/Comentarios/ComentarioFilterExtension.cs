using Domain.Common.Extension;
using Domain.Entities;
using Domain.Filter.Filters.Comentarios;
using System.Linq;

namespace Infrastructure.Data.Repository.Comentarios
{
    public static class ComentarioFilterExtension
    {
        public static IQueryable<Comentario> WithFilters(this IQueryable<Comentario> queryBase, ComentarioFilter filters)
        {
            var queryFilter = queryBase;

            if (filters.Id.HasValue)
            {
                queryFilter = queryFilter.Where(_ => _.Id == filters.Id.Value);
            }
            if (filters.TarefaId.HasValue)
            {
                queryFilter = queryFilter.Where(_ => _.TarefaId == filters.TarefaId.Value);
            }
            if (!string.IsNullOrEmpty(filters.Autor))
            {
                queryFilter = queryFilter.Where(_ => _.Autor.Contains(filters.Autor));
            }
            if (filters.DataCriacaoInicio.HasValue)
            {
                queryFilter = queryFilter.Where(_ => _.DataCriacao >= filters.DataCriacaoInicio.Value);
            }
            if (filters.DataCriacaoFim.HasValue)
            {
                // Considera o fim do dia para DataCriacaoFim
                var dataFim = filters.DataCriacaoFim.Value.AddDays(1).AddMilliseconds(-1);
                queryFilter = queryFilter.Where(_ => _.DataCriacao <= dataFim);
            }

            return queryFilter;
        }

        public static IQueryable<Comentario> OrderByDomain(this IQueryable<Comentario> queryBase, ComentarioFilter filters)
        {
            return queryBase.OrderBy(_ => _.Id);
        }
    }
}
