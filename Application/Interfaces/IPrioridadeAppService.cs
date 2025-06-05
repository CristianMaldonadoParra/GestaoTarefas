using Application.Dto.Dtos.Prioridades;
using Application.Dto.Helper;
using Domain.Common.Interfaces;
using Domain.Common.Model;
using Domain.Filter.Filters.Prioridades;

namespace Application.Interfaces
{
    public interface IPrioridadeAppService : IApplicationServiceBase<PaginateResult<PrioridadeModel>,
                                          ModelResult<PrioridadeModel>,
                                          ModelResult<PrioridadeIdModel>,
                                          PrioridadeFilter,
                                          PrioridadeDto>, IDisposable
    {
    }
}
