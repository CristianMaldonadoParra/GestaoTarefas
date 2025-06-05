using Application.Dto.Dtos.HistoricoAtualizacoes;
using FluentValidation;

namespace Application.Validators
{
    public class HistoricoAtualizacaoDtoValidator : AbstractValidator<HistoricoAtualizacaoDto>
    {
        public HistoricoAtualizacaoDtoValidator()
        {
            RuleFor(x => x.TarefaId).GreaterThan(0).WithMessage("TarefaId � obrigat�rio.");
            RuleFor(x => x.Autor).NotEmpty().MaximumLength(100).WithMessage("Autor � obrigat�rio e deve ter at� 100 caracteres.");
            RuleFor(x => x.CampoAlterado).NotEmpty().MaximumLength(100).WithMessage("CampoAlterado � obrigat�rio e deve ter at� 100 caracteres.");
        }
    }
}
