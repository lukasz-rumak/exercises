using System;
using System.ComponentModel.DataAnnotations;
using BoardGameApi.DataAnnotations;

namespace BoardGameApi.Models
{
    public class MovePlayer
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid PlayerId as integer")]
        public int PlayerId { get; set; }
        [Required]
        [MoveValidator(ErrorMessage = "Only following commands are available: M - move, L - turn left, R - turn right")]
        public string Move { get; set; }
    }
}