using Domain.Entities;
using Domain.Filter.Filters.Tarefas;
using System.Linq;

namespace Infrastructure.Data.Repository.Tarefas
{
    public static class TarefaFilterExtension
    {
        public static IQueryable<Tarefa> WithFilters(this IQueryable<Tarefa> queryBase, TarefaFilter filters)
        {
            var queryFilter = queryBase;

            if (filters.Id.HasValue)
                queryFilter = queryFilter.Where(_ => _.Id == filters.Id);

            if (filters.ProjetoId.HasValue)
                queryFilter = queryFilter.Where(_ => _.ProjetoId == filters.ProjetoId);

            if (!string.IsNullOrEmpty(filters.Titulo))
                queryFilter = queryFilter.Where(_ => _.Titulo.Contains(filters.Titulo));

            if (filters.StatusId.HasValue)
                queryFilter = queryFilter.Where(_ => _.StatusId == filters.StatusId);

            if (filters.PrioridadeId.HasValue)
                queryFilter = queryFilter.Where(_ => _.PrioridadeId == filters.PrioridadeId);

            if (filters.DataCriacaoInicio.HasValue)
                queryFilter = queryFilter.Where(_ => _.DataCriacao >= filters.DataCriacaoInicio);

            if (filters.DataCriacaoFim.HasValue)
            {
                var dataFim = filters.DataCriacaoFim.Value.AddDays(1).AddMilliseconds(-1);
                queryFilter = queryFilter.Where(_ => _.DataCriacao <= dataFim);
            }

            return queryFilter;
        }

        public static IQueryable<Tarefa> OrderByDomain(this IQueryable<Tarefa> queryBase, TarefaFilter filters)
        {
            return queryBase.OrderBy(_ => _.Id);
        }
    }
}
