namespace Domain.Entities
{
    public class Projeto
    {
        public Projeto() { }

        public Projeto(string nome, string? descricao, DateTime dataCriacao)
        {
            Nome = nome;
            Descricao = descricao;
            DataCriacao = dataCriacao;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }

        public void SetId(int id) => Id = id;
        public void SetNome(string nome) => Nome = nome;
        public void SetDescricao(string? descricao) => Descricao = descricao;
        public void SetDataCriacao(DateTime dataCriacao) => DataCriacao = dataCriacao;
    }
}
