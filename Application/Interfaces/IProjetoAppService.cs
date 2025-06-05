using Application.Dto.Dtos.Projetos;
using Application.Dto.Helper;
using Domain.Common.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.Projetos;

namespace Application.Interfaces
{
    public interface IProjetoAppService : IApplicationServiceBase<PaginateResult<ProjetoModel>,
                                          ModelResult<ProjetoModel>,
                                          ModelResult<ProjetoIdModel>,
                                          ProjetosFilter,
                                          ProjetoDto>, IDisposable
    {
    }
}
