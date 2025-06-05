using Application.Dto.Dtos.StatusTarefas;
using Application.Dto.Helper;
using Domain.Common.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.StatusTarefas;

namespace Application.Interfaces
{
    public interface IStatusTarefaAppService : IApplicationServiceBase<PaginateResult<StatusTarefaModel>,
                                          ModelResult<StatusTarefaModel>,
                                          ModelResult<StatusTarefaIdModel>,
                                          StatusTarefaFilter,
                                          StatusTarefaDto>, IDisposable
    {
    }
}
