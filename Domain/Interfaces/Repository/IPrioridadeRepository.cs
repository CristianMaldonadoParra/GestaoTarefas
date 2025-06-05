using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Filter.Filters.Prioridades;

namespace Domain.Interfaces.Repository
{
    public interface IPrioridadeRepository : IRepository<Prioridade, PrioridadeFilter>
    {
    }
}
