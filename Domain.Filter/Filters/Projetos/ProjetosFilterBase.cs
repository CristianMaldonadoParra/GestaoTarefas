using Domain.Common.Filters;

namespace Domain.Filter.Filters.Projetos
{
    public class ProjetosFilterBase : FilterBase
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataCriacaoInicio { get; set; }
        public DateTime? DataCriacaoFim { get; set; }
    }
}
