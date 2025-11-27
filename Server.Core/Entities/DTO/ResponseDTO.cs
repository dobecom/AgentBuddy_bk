namespace Server.Core.Entities.DTO
{
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public ErrorDTO? Error { get; set; }
    }

    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ErrorDTO? Error { get; set; }
    }

    public class ErrorDTO
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
    }
}
