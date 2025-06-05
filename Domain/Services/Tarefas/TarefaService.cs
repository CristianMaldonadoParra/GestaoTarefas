using Application.Dto.Dtos.Tarefas;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Tarefas
{
    public class TarefaService : TarefaServiceBase
    {
        public TarefaService(ITarefaRepository tarefaRepository, IValidator<TarefaDto> validator)
            : base(tarefaRepository, validator)
        {
        }
    }
}
