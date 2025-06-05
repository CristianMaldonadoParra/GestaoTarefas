using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Filter.Filters.StatusTarefa;

namespace Domain.Interfaces.Repository
{
    public interface IStatusTarefaRepository : IRepository<StatusTarefa, StatusTarefaFilter>
    {
    }
}
