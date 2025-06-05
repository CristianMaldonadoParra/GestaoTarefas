namespace Application.Dto.Dtos.HistoricoAtualizacoes
{
    public class HistoricoAtualizacaoModel
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
