namespace Application.Dto.Dtos.Tarefas
{
    public class TarefaModel
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int StatusId { get; set; }
        public int PrioridadeId { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class TarefaIdModel
    {
        public int Id { get; set; }
    }
}
