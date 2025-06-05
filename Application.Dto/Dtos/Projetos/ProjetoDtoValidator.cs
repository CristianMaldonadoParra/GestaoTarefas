using FluentValidation;

namespace Application.Dto.Dtos.Projetos
{
    public class ProjetoDtoValidator : AbstractValidator<ProjetoDto>
    {
        public ProjetoDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome � obrigat�rio.")
                .MaximumLength(200).WithMessage("O nome deve ter no m�ximo 200 caracteres.");

            RuleFor(x => x.DataCriacao)
                .NotEmpty().WithMessage("A data de cria��o � obrigat�ria.");
        }
    }
}
