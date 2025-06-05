namespace Application.Dto.Dtos.Comentarios
{
    public class ComentarioDto
    {
        public int Id { get; set; }
        public int TarefaId { get; set; }
        public string Autor { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
