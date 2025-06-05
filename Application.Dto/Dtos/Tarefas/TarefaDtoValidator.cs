using FluentValidation;

namespace Application.Dto.Dtos.Tarefas
{
    public class TarefaDtoValidator : AbstractValidator<TarefaDto>
    {
        public TarefaDtoValidator()
        {
            RuleFor(x => x.ProjetoId)
                .NotEmpty().WithMessage("O Projeto � obrigat�rio.");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O t�tulo � obrigat�rio.")
                .MaximumLength(200).WithMessage("O t�tulo deve ter no m�ximo 200 caracteres.");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("O status � obrigat�rio.");

            RuleFor(x => x.PrioridadeId)
                .NotEmpty().WithMessage("A prioridade � obrigat�ria.");

            RuleFor(x => x.DataCriacao)
                .NotEmpty().WithMessage("A data de cria��o � obrigat�ria.");
        }
    }
}
