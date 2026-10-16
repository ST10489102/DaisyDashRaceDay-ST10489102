using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class Jockey
    {
        [Key]
        public int JockeyId { get; set; }

        [Required]
        public string FullName { get; set; } = "";

        public int ExperienceYears { get; set; }
    }
}