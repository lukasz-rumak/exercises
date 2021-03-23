using System;
using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.Models
{
    public class Wall
    {
        [Required]
        public string WallCoordinates { get; set; }
    }
}