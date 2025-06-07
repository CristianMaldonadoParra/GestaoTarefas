using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Filter.Filters.Tarefas;

namespace Domain.Interfaces.Repository
{
    public interface ITarefaRepository : IRepository<Tarefa, TarefaFilter>
    {
        Task<List<Tarefa>> ObterMediaTarefasConcluidasPorUsuarioUltimos30DiasAsync();
    }
}
