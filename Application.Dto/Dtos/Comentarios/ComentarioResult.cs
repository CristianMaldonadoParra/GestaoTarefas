using FluentValidation.Results;

namespace Application.Dto.Dtos.Comentarios
{
    public class ComentarioResult
    {
        public ComentarioDto Comentario { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public string Message { get; set; }
    }
}
