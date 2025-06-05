using FluentValidation.Results;

namespace Application.Dto.Dtos.Prioridades
{
    public class PrioridadeResult
    {
        public PrioridadeDto Prioridade { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public string Message { get; set; }
    }
}
