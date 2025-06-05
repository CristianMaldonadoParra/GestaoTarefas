using FluentValidation;

namespace Application.Dto.Dtos.Comentarios
{
    public class ComentarioDtoValidator : AbstractValidator<ComentarioDto>
    {
        public ComentarioDtoValidator()
        {
            RuleFor(x => x.TarefaId).GreaterThan(0).WithMessage("TarefaId � obrigat�rio.");
            RuleFor(x => x.Autor).NotEmpty().MaximumLength(100).WithMessage("Autor � obrigat�rio e deve ter at� 100 caracteres.");
            RuleFor(x => x.Texto).NotEmpty().WithMessage("Texto � obrigat�rio.");
        }
    }
}
