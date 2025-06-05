using Domain.Common.Filters;

namespace Domain.Filter.Filters.StatusTarefas
{
    public class StatusTarefaFilterBase : FilterBase
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
    }
}
