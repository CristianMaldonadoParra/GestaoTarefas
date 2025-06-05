using Application.Dto.Dtos.Comentarios;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Comentarios
{
    public class ComentarioService : ComentarioServiceBase
    {
        public ComentarioService(IComentarioRepository comentarioRepository, IValidator<ComentarioDto> validator)
            : base(comentarioRepository, validator)
        {
        }
    }
}
