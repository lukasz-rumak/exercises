using System;
using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class MovePlayer
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public string Move { get; set; }
    }
}