using Application.Dto.Dtos.Projetos;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Projetos
{

    public class ProjetoService : ProjetoServiceBase
    {
        public ProjetoService(IProjetoRepository projetoRepository, IValidator<ProjetoDto> validator) : base(projetoRepository, validator)
        {
        }
    }
}
