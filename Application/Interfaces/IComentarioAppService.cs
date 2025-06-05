using Application.Dto.Dtos.Comentarios;
using Domain.Filter.Filters.Comentarios;
using Domain.Common.Model;
using Application.Dto.Helper;

namespace Application.Interfaces
{
    public interface IComentarioAppService : IDisposable
    {
        Task<ModelResult<ComentarioModel>> GetByIdAsync(int id);
        Task<PaginateResult<ComentarioModel>> GetWithFilters(ComentarioFilter filters);
        Task<ModelResult<ComentarioModel>> CreateAsync(ComentarioDto dto);
        Task<ModelResult<ComentarioModel>> UpdateAsync(ComentarioDto dto);
        Task<ModelResult<ComentarioModel>> DeleteAsync(int id);
    }
}
