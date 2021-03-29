using System.ComponentModel.DataAnnotations;
using BoardGameApi.Validators;

namespace BoardGameApi.Models
{
    public class MovePlayer
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid PlayerId as integer")]
        public int PlayerId { get; set; }
        [Required]
        [MoveTo(ErrorMessage = "Only following commands are available: M - move, L - turn left, R - turn right")]
        public string MoveTo { get; set; }
    }
}