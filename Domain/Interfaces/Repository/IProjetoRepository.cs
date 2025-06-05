using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Filter.Filters.Projetos;

namespace Domain.Interfaces.Repository
{
    public interface IProjetoRepository : IRepository<Projeto, ProjetosFilter>
    {
        
    }
}
