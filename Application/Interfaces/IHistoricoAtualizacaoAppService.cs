using Application.Dto.Dtos.HistoricoAtualizacoes;
using Domain.Filter.Filters.HistoricoAtualizacoes;
using Domain.Common.Model;
using Application.Dto.Helper;

namespace Application.Interfaces
{
    public interface IHistoricoAtualizacaoAppService : IDisposable
    {
        Task<ModelResult<HistoricoAtualizacaoModel>> GetByIdAsync(int id);
        Task<PaginateResult<HistoricoAtualizacaoModel>> GetWithFilters(HistoricoAtualizacaoFilter filters);
        Task<ModelResult<HistoricoAtualizacaoModel>> CreateAsync(HistoricoAtualizacaoDto dto);
        Task<ModelResult<HistoricoAtualizacaoModel>> UpdateAsync(HistoricoAtualizacaoDto dto);
        Task<ModelResult<HistoricoAtualizacaoModel>> DeleteAsync(int id);
    }
}
