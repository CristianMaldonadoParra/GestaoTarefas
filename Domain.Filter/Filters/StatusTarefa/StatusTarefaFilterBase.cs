using Domain.Common.Filters;

namespace Domain.Filter.Filters.StatusTarefa
{
    public class StatusTarefaFilterBase : FilterBase
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
    }
}
