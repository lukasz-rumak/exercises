using System;
using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class Wall
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public string WallCoordinates { get; set; }
    }
}