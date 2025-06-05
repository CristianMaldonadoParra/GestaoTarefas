using FluentValidation.Results;

namespace Application.Dto.Dtos.StatusTarefas
{
    public class StatusTarefaResult
    {
        public StatusTarefaDto StatusTarefa { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public string Message { get; set; }
    }
}
