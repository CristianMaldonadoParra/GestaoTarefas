using Application.Dto.Dtos.StatusTarefas;
using Domain.Interfaces.Repository;
using Domain.Services.StatusTarefas;
using FluentValidation;

namespace Domain.Services.StatusTarefas
{
    public class StatusTarefaService : StatusTarefaServiceBase
    {
        public StatusTarefaService(IStatusTarefaRepository statusTarefaRepository, IValidator<StatusTarefaDto> validator)
            : base(statusTarefaRepository, validator)
        {
        }
    }
}
