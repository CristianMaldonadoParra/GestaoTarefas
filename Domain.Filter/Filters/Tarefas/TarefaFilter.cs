using Domain.Common.Filters;

namespace Domain.Filter.Filters.Tarefas
{
    public class TarefaFilter : FilterBase
    {
        public int? Id { get; set; }
        public int? ProjetoId { get; set; }
        public string? Titulo { get; set; }
        public int? StatusId { get; set; }
        public int? PrioridadeId { get; set; }
        public DateTime? DataCriacaoInicio { get; set; }
        public DateTime? DataCriacaoFim { get; set; }
    }
}
