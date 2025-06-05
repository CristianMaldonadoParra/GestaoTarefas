using FluentValidation;

namespace Application.Dto.Dtos.Projetos
{
    public class ProjetoDtoValidator : AbstractValidator<ProjetoDto>
    {
        public ProjetoDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(200).WithMessage("O nome deve ter no máximo 200 caracteres.");

            RuleFor(x => x.DataCriacao)
                .NotEmpty().WithMessage("A data de criação é obrigatória.");
        }
    }
}
