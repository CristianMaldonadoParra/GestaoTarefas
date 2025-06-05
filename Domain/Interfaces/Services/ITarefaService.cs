using Application.Dto.Dtos.Tarefas;
using Domain.Filter.Filters.Tarefas;

namespace Domain.Interfaces.Services
{
    public interface ITarefaService
    {
        Task<TarefaResult> Create(TarefaDto dto);
        Task<TarefaResult> GetById(int id);
        Task<(List<TarefaDto> Result, int TotalCount)> GetWithFilters(TarefaFilter filter);
        Task<TarefaResult> Update(TarefaDto dto);
        Task<TarefaResult> Delete(int id);
    }
}
