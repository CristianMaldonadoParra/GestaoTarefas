using Application.Dto.Dtos.HistoricoAtualizacoes;
using Domain.Filter.Filters.HistoricoAtualizacoes;

namespace Domain.Interfaces.Services
{
    public interface IHistoricoAtualizacaoService
    {
        Task<HistoricoAtualizacaoResult> GetById(int id);
        Task<(List<HistoricoAtualizacaoDto> Result, int TotalCount)> GetWithFilters(HistoricoAtualizacaoFilter filter);
        Task<HistoricoAtualizacaoResult> Create(HistoricoAtualizacaoDto dto);
        Task<HistoricoAtualizacaoResult> Update(HistoricoAtualizacaoDto dto);
        Task<HistoricoAtualizacaoResult> Delete(int id);
    }
}
