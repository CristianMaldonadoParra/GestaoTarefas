using FluentValidation;

namespace Application.Dto.Dtos.StatusTarefas
{
    public class StatusTarefaDtoValidator : AbstractValidator<StatusTarefaDto>
    {
        public StatusTarefaDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome � obrigat�rio.")
                .MaximumLength(50).WithMessage("O nome deve ter no m�ximo 50 caracteres.");
        }
    }
}
