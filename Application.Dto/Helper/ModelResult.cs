namespace Application.Dto.Helper
{
    public class ModelResult<TModel>
    {
        public TModel? Model { get; set; }
        public ICollection<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; } = string.Empty;
    }
}
