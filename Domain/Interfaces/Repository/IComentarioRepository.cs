using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Filter.Filters.Comentarios;

namespace Domain.Interfaces.Repository
{
    public interface IComentarioRepository : IRepository<Comentario, ComentarioFilter>
    {

    }
}
