namespace Domain.Entities
{
    public class StatusTarefa
    {
        public StatusTarefa() { }

        public StatusTarefa(string nome)
        {
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public void SetNome(string nome) => Nome = nome;
    }
}
