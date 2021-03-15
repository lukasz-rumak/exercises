using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class Board
    {
        [Required]
        public int WithSize { get; set; }
        public Wall Wall { get; set; }
    }
}