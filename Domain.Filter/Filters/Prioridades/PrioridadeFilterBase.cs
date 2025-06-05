using Domain.Common.Filters;

namespace Domain.Filter.Filters.Prioridades
{
    public class PrioridadeFilterBase : FilterBase
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
    }
}
