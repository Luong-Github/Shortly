namespace Shortly.API.Models
{
    public class ApiReponse<T>
    {
        public bool Success { get; set; }
        public string? StatusCode { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Errors { get; set; }
    }
}
