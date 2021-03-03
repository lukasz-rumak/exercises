using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class GameInit
    {
        [Required]
        public Board Board { get; set; }
    }
}