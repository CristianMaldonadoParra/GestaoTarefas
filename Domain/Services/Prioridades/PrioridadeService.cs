using Application.Dto.Dtos.Prioridades;
using Domain.Interfaces.Repository;
using FluentValidation;

namespace Domain.Services.Prioridades
{
    public class PrioridadeService : PrioridadeServiceBase
    {
        public PrioridadeService(IPrioridadeRepository prioridadeRepository, IValidator<PrioridadeDto> validator)
            : base(prioridadeRepository, validator)
        {
        }
    }
}
