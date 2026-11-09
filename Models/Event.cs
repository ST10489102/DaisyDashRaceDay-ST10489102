using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

        [Required]
        public string Location { get; set; } = "";

        [Required]
        public DateTime EventDate { get; set; }

        // Part 3 required: e.g. 5, 10, 21, 42 - in km
        public double Distance { get; set; }

        // Part 3 required: must be "Run", "Walk" or "Cycle" - used for filtering on Home page
        [Required]
        public string EventType { get; set; } = "Run";

        public decimal EntryFee { get; set; }

        // Part 3: Event banner image - Blob URL saved here
        public string? ImageUrl { get; set; }

        // Part 3: Who created this event - needed for Organiser Dashboard
        public int OrganiserId { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}