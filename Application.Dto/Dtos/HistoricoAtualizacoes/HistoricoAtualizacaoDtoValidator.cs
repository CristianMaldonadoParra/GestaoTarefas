using Application.Dto.Dtos.HistoricoAtualizacoes;
using FluentValidation;

namespace Application.Validators
{
    public class HistoricoAtualizacaoDtoValidator : AbstractValidator<HistoricoAtualizacaoDto>
    {
        public HistoricoAtualizacaoDtoValidator()
        {
            RuleFor(x => x.TarefaId).GreaterThan(0).WithMessage("TarefaId é obrigatório.");
            RuleFor(x => x.Autor).NotEmpty().MaximumLength(100).WithMessage("Autor é obrigatório e deve ter até 100 caracteres.");
            RuleFor(x => x.CampoAlterado).NotEmpty().MaximumLength(100).WithMessage("CampoAlterado é obrigatório e deve ter até 100 caracteres.");
        }
    }
}
