using System.ComponentModel;

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

    public class BadRequestErrors
    {
        public string[] Errors { get; set; }
    }

    public class BadRequestWithSize
    {
        public string[] WithSize { get; set; }
    }
}