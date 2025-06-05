using Domain.Entities;
using Domain.Filter.Filters.HistoricoAtualizacoes;

namespace Infrastructure.Data.Repository.HistoricoAtualizacoes
{
    public static class HistoricoAtualizacaoFilterExtension
    {
        public static IQueryable<HistoricoAtualizacao> WithFilters(this IQueryable<HistoricoAtualizacao> queryBase, HistoricoAtualizacaoFilter filters)
        {
            var queryFilter = queryBase;

            if (filters.Id > 0)
                queryFilter = queryFilter.Where(_ => _.Id == filters.Id);


            if (filters.TarefaId > 0)
                queryFilter = queryFilter.Where(_ => _.TarefaId == filters.TarefaId);


            if (!string.IsNullOrWhiteSpace(filters.Autor))
                queryFilter = queryFilter.Where(_ => _.Autor.Contains(filters.Autor));


            if (!string.IsNullOrWhiteSpace(filters.CampoAlterado))
                queryFilter = queryFilter.Where(_ => _.CampoAlterado.Contains(filters.CampoAlterado));


            if (!string.IsNullOrWhiteSpace(filters.ValorAntigo))
                queryFilter = queryFilter.Where(_ => _.ValorAntigo != null && _.ValorAntigo.Contains(filters.ValorAntigo));


            if (!string.IsNullOrWhiteSpace(filters.ValorNovo))
                queryFilter = queryFilter.Where(_ => _.ValorNovo != null && _.ValorNovo.Contains(filters.ValorNovo));

            
            if (filters.DataAlteracaoInicio.HasValue)
                queryFilter = queryFilter.Where(_ => _.DataAlteracao >= filters.DataAlteracaoInicio.Value);

            if (filters.DataAlteracaoFim.HasValue)
            {
                var dataFim = filters.DataAlteracaoFim.Value.AddDays(1).AddMilliseconds(-1);
                queryFilter = queryFilter.Where(_ => _.DataAlteracao <= dataFim);
            }

            return queryFilter;
        }

        public static IQueryable<HistoricoAtualizacao> OrderByDomain(this IQueryable<HistoricoAtualizacao> queryBase, HistoricoAtualizacaoFilter filters)
        {
            return queryBase.OrderBy(_ => _.Id);
        }
    }
}
