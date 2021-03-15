using System;
using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class Session
    {
        [Required]
        public Guid SessionId { get; set; }
    }
}