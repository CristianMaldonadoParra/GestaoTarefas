using FluentValidation;

namespace Application.Dto.Dtos.Tarefas
{
    public class TarefaDtoValidator : AbstractValidator<TarefaDto>
    {
        public TarefaDtoValidator()
        {
            RuleFor(x => x.ProjetoId)
                .NotEmpty().WithMessage("O Projeto é obrigatório.");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(200).WithMessage("O título deve ter no máximo 200 caracteres.");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("O status é obrigatório.");

            RuleFor(x => x.PrioridadeId)
                .NotEmpty().WithMessage("A prioridade é obrigatória.");

            RuleFor(x => x.DataCriacao)
                .NotEmpty().WithMessage("A data de criação é obrigatória.");
        }
    }
}
