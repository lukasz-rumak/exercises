using System.ComponentModel.DataAnnotations;
using BoardGameApi.Validators;

namespace BoardGameApi.Models
{
    public class Berry
    {
        [Required]
        [BerryCoordinates(ErrorMessage = "Please enter valid berry coordinates format. For example: 'B 1 2'")]
        public string BerryCoordinates { get; set; }
    }
}