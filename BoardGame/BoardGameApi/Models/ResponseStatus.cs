using System;

namespace BoardGameApi.Models
{
    public class ResponseStatus
    {
        public Guid SessionId { get; set; }
        public string Status { get; set; }
    }
}