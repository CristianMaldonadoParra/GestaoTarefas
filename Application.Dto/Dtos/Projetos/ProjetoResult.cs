using FluentValidation.Results;

namespace Application.Dto.Dtos.Projetos
{
    public class ProjetoResult
    {
        public ProjetoDto Projeto { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public string Message { get; set; }
    }
}
