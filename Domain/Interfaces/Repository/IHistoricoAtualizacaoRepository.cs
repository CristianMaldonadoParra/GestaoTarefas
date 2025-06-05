using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Filter.Filters.HistoricoAtualizacoes;

namespace Domain.Interfaces.Repository
{
    public interface IHistoricoAtualizacaoRepository : IRepository<HistoricoAtualizacao, HistoricoAtualizacaoFilter>
    {
        
    }
}
