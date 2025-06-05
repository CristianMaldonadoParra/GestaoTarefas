using Application.Dto.Dtos.Tarefas;
using Application.Dto.Helper;
using Domain.Common.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.Tarefas;

namespace Application.Interfaces
{
    public interface ITarefaAppService : IApplicationServiceBase<PaginateResult<TarefaModel>,
                                          ModelResult<TarefaModel>,
                                          ModelResult<TarefaIdModel>,
                                          TarefaFilter,
                                          TarefaDto>, IDisposable
    {
    }
}
