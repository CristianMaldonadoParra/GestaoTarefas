namespace Domain.Entities
{
    public class Tarefa
    {
        public Tarefa() { }

        public Tarefa(int projetoId, string titulo, string? descricao, DateTime? dataVencimento, int statusId, int prioridadeId, DateTime dataCriacao, string autor)
        {
            ProjetoId = projetoId;
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            StatusId = statusId;
            PrioridadeId = prioridadeId;
            DataCriacao = dataCriacao;
            Autor = autor;
        }

        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string Autor { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int StatusId { get; set; }
        public int PrioridadeId { get; set; }
        public DateTime DataCriacao { get; set; }

        public void SetId(int id) => Id = id;
        public void SetTitulo(string titulo) => Titulo = titulo;
        public void SetDescricao(string? descricao) => Descricao = descricao;
        public void SetDataVencimento(DateTime? dataVencimento) => DataVencimento = dataVencimento;
        public void SetStatusId(int statusId) => StatusId = statusId;
        public void SetPrioridadeId(int prioridadeId) => PrioridadeId = prioridadeId;
        public void SetAutor(string autor) => Autor = autor;
    }
}
