namespace Application.Dto.Dtos.Tarefas
{
    public class TarefaDto : DtoBase
    {
        public virtual int Id { get; set; }
        public virtual int ProjetoId { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string? Descricao { get; set; }
        public virtual DateTime? DataVencimento { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int PrioridadeId { get; set; }
        public virtual DateTime DataCriacao { get; set; }
    }
}
