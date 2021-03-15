using System;
using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class AddPlayer
    {
        [Required]
        public Guid SessionId { get; set; }
        public int PlayerId { get; set; }
        [Required]
        public string PlayerType { get; set; } // TODO
        public string StartPosition { get; set; }
    }
}