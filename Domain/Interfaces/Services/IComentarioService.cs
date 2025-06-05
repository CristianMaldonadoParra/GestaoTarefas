using Application.Dto.Dtos.Comentarios;
using Domain.Filter.Filters.Comentarios;

namespace Domain.Interfaces.Services
{
    public interface IComentarioService
    {
        Task<ComentarioResult> GetById(int id);
        Task<(List<ComentarioDto> Result, int TotalCount)> GetWithFilters(ComentarioFilter filter);
        Task<ComentarioResult> Create(ComentarioDto dto);
        Task<ComentarioResult> Update(ComentarioDto dto);
        Task<ComentarioResult> Delete(int id);
    }
}
