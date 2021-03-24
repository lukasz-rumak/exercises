namespace BoardGameApi.Models
{
    public class BadRequestResponse<T> where T : class
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public T Errors { get; set; }
    }

    public class WithSize
    {
        public string IDoNotKnow { get; set; }
    }
}