using System;

namespace BoardGameApi.Models
{
    public class GenericResponse
    {
        public Guid SessionId { get; set; }
        public string Response { get; set; }
    }
}