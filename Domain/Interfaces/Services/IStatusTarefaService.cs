using Application.Dto.Dtos.StatusTarefas;
using Domain.Filter.Filters.StatusTarefa;

namespace Domain.Interfaces.Services
{
    public interface IStatusTarefaService
    {
        Task<StatusTarefaResult> Create(StatusTarefaDto dto);
        Task<StatusTarefaResult> GetById(int id);
        Task<(List<StatusTarefaDto> Result, int TotalCount)> GetWithFilters(StatusTarefaFilter filter);
        Task<StatusTarefaResult> Update(StatusTarefaDto dto);
        Task<StatusTarefaResult> Delete(int id);
    }
}
