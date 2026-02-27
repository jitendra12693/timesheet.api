namespace timesheet.business.Dtos
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? TraceId { get; set; }
        public object? Errors { get; set; }
    }
}
