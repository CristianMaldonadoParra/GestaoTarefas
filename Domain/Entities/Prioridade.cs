namespace Domain.Entities
{
    public class Prioridade
    {
        public Prioridade() { }

        public Prioridade(string nome)
        {
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public void SetNome(string nome) => Nome = nome;
    }
}
