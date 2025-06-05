using FluentValidation;

namespace Application.Dto.Dtos.Comentarios
{
    public class ComentarioDtoValidator : AbstractValidator<ComentarioDto>
    {
        public ComentarioDtoValidator()
        {
            RuleFor(x => x.TarefaId).GreaterThan(0).WithMessage("TarefaId é obrigatório.");
            RuleFor(x => x.Autor).NotEmpty().MaximumLength(100).WithMessage("Autor é obrigatório e deve ter até 100 caracteres.");
            RuleFor(x => x.Texto).NotEmpty().WithMessage("Texto é obrigatório.");
        }
    }
}
