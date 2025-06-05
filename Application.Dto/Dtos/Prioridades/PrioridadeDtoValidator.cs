using FluentValidation;

namespace Application.Dto.Dtos.Prioridades
{
    public class PrioridadeDtoValidator : AbstractValidator<PrioridadeDto>
    {
        public PrioridadeDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(50).WithMessage("O nome deve ter no máximo 50 caracteres.");
        }
    }
}
