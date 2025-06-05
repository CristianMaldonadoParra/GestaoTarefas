namespace Domain.Filter.Filters.HistoricoAtualizacoes
{
    public class HistoricoAtualizacaoFilter : HistoricoAtualizacaoFilterBase
    {
        public DateTime? DataAlteracaoInicio { get; set; }
        public DateTime? DataAlteracaoFim { get; set; }
    }
}
