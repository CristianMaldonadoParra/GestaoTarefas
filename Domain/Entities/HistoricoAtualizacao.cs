namespace Domain.Entities
{
    public class HistoricoAtualizacao
    {
        public int Id { get; set; }
        public int TarefaId { get; set; }
        public string Autor { get; set; }
        public string CampoAlterado { get; set; }
        public string? ValorAntigo { get; set; }
        public string? ValorNovo { get; set; }
        public DateTime DataAlteracao { get; set; }

        public HistoricoAtualizacao() { }

        public HistoricoAtualizacao(int tarefaId, string autor, string campoAlterado, string? valorAntigo, string? valorNovo)
        {
            TarefaId = tarefaId;
            Autor = autor;
            CampoAlterado = campoAlterado;
            ValorAntigo = valorAntigo;
            ValorNovo = valorNovo;
            DataAlteracao = DateTime.Now;
        }
    }
}
