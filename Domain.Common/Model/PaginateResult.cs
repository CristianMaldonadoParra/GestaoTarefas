namespace Domain.Common.Model
{
    public class PaginateResult<T>
    {
        public IEnumerable<T> ResultData { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
