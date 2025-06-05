using FluentValidation.Results;

namespace Application.Dto.Dtos.HistoricoAtualizacoes
{
    public class HistoricoAtualizacaoResult
    {
        public HistoricoAtualizacaoDto HistoricoAtualizacao { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public string Message { get; set; }
    }
}
