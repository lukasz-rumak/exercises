using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class Board
    {
        [Required]
        [Range(3, 20, ErrorMessage = "Please enter valid board size from 3 to 20")]
        public int WithSize { get; set; }
    }
}