using FluentValidation;

namespace Application.Dto.Dtos.StatusTarefas
{
    public class StatusTarefaDtoValidator : AbstractValidator<StatusTarefaDto>
    {
        public StatusTarefaDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(50).WithMessage("O nome deve ter no máximo 50 caracteres.");
        }
    }
}
