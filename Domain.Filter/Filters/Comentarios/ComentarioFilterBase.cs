using Domain.Common.Filters;

namespace Domain.Filter.Filters.Comentarios
{
    public class ComentarioFilterBase: FilterBase
    {
        public int? Id { get; set; }
        public int? TarefaId { get; set; }
        public string Autor { get; set; }
        public DateTime? DataCriacaoInicio { get; set; }
        public DateTime? DataCriacaoFim { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
