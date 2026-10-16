using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }

        [Required]
        public string RaceName { get; set; } = "";

        public string Location { get; set; } = "";

        public DateTime RaceDate { get; set; }

        public decimal PrizeMoney { get; set; }
    }
}