using Domain.Common.Filters;

namespace Domain.Filter.Filters.HistoricoAtualizacoes
{
    public class HistoricoAtualizacaoFilterBase : FilterBase
    {
        public int Id { get; set; }
        public int TarefaId { get; set; }
        public string Autor { get; set; }
        public string CampoAlterado { get; set; }
        public string? ValorAntigo { get; set; }
        public string? ValorNovo { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
