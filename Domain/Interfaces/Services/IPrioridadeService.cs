using Application.Dto.Dtos.Prioridades;
using Domain.Filter.Filters.Prioridades;

namespace Domain.Interfaces.Services
{
    public interface IPrioridadeService
    {
        Task<PrioridadeResult> Create(PrioridadeDto dto);
        Task<PrioridadeResult> GetById(int id);
        Task<(List<PrioridadeDto> Result, int TotalCount)> GetWithFilters(PrioridadeFilter filter);
        Task<PrioridadeResult> Update(PrioridadeDto dto);
        Task<PrioridadeResult> Delete(int id);
    }
}
