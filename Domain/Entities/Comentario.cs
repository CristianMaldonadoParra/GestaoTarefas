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

        public Comentario(int tarefaId, string texto)
        {
            TarefaId = tarefaId;
            Texto = texto;
            DataCriacao = DateTime.Now;
        }

        public void SetTexto(string texto)
        {
            Texto = texto;
        }
        public void SetAutor(string value)
        {
            Texto = value;
        }
    }
}
