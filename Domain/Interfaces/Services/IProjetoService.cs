using Application.Dto.Dtos.Projetos;
using Domain.Filter.Filters.Projetos;

namespace Domain.Interfaces.Services
{
    public interface IProjetoService
    {
        Task<ProjetoResult> Create(ProjetoDto dto);
        Task<ProjetoResult> GetById(int id);
        Task<(List<ProjetoDto> Result, int TotalCount)> GetWithFilters(ProjetosFilter filter);
        Task<ProjetoResult> Update(ProjetoDto dto);
        Task<ProjetoResult> Delete(int id);
    }
}
