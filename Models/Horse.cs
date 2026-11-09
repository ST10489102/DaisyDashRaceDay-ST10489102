using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class Horse
    {
        [Key]
        public int HorseId { get; set; }

        [Required]
        public string HorseName { get; set; } = "";

        public string Breed { get; set; } = "";

        public int Age { get; set; }
    }
}