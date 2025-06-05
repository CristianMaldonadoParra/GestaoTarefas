using FluentValidation.Results;

namespace Application.Dto.Dtos.Tarefas
{
    public class TarefaResult
    {
        public TarefaDto Tarefa { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public string Message { get; set; }
    }
}
