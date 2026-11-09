using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.MVC.Models
{
    public class EntryViewModel
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Participant Name")]
        public string ParticipantName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        // For dropdown in Create view
        public List<CategoryViewModel> Categories { get; set; } = new();

        // For display in Index view
        public string? CategoryName { get; set; }
        public string? EventName { get; set; }
    }
}