using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class Board
    {
        [Required]
        [Range(2, 20, ErrorMessage = "Please enter valid board size from 2 to 20")]
        public int WithSize { get; set; }
    }
}