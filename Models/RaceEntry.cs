using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class RaceEntry
    {
        [Key]
        public int EntryId { get; set; }

        public int RaceId { get; set; }

        public int HorseId { get; set; }

        public int JockeyId { get; set; }

        public int Position { get; set; }
    }
}