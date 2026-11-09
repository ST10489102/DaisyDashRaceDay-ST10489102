using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.MVC.Models
{
    public class ResultsViewModel
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please select an entry")]
        [Display(Name = "Participant")]
        public int EntryId { get; set; }

        [Required]
        [Display(Name = "Finish Time (e.g. 00:42:15)")]
        public string FinishTime { get; set; } = "";

        [Required]
        [Range(1, 1000)]
        public int Position { get; set; }

        // For dropdown in Create view
        public List<EntryViewModel> Entries { get; set; } = new();

        // For display in Index view
        public string? ParticipantName { get; set; }
    }
}