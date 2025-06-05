namespace Domain.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public int TarefaId { get; set; }
        public string Autor { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }

        public Comentario() { }

        public Comentario(int tarefaId, string autor, string texto)
        {
            TarefaId = tarefaId;
            Autor = autor;
            Texto = texto;
            DataCriacao = DateTime.Now;
        }

        public void SetTexto(string texto)
        {
            Texto = texto;
        }
    }
}
