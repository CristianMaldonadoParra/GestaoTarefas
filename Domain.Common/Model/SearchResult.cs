namespace Domain.Common.Model
{
    public class SearchResult<T>
    {
        public IEnumerable<T> DataList { get; set; }

        public Summary Summary { get; set; }

    }
}
