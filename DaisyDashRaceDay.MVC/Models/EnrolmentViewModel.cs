namespace DaisyDashRaceDay.MVC.Models;

public class EnrolmentViewModel
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int CategoryId { get; set; }
    public int ParticipantId { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed
    public DateTime EnrolledOn { get; set; } = DateTime.Now;

    // For display only - filled from API
    public string? EventName { get; set; }
    public string? CategoryName { get; set; }
    public string? ParticipantName { get; set; }
}