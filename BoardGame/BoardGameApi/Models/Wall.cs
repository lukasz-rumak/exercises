using System;
using System.ComponentModel.DataAnnotations;
using BoardGameApi.Validators;

namespace BoardGameApi.Models
{
    public class Wall
    {
        [Required]
        [WallCoordinates(ErrorMessage = "Please enter valid wall coordinates format. For example: W 1 1 2 1")]
        public string WallCoordinates { get; set; }
    }
}