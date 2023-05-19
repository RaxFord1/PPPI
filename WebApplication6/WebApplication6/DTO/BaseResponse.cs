namespace PracticeAPI.DTO
{
    public class BaseResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public int ValueCount { get; set; } = 0;
        public List<T> Values { get; set; } = new List<T>();
    }
}
