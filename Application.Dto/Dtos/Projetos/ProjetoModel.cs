namespace Application.Dto.Dtos.Projetos
{
    public class ProjetoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class ProjetoIdModel
    {
        public int Id { get; set; }
    }
}
