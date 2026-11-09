namespace DaisyDashRaceDay.API.Models
{
    public class Enrolment
    {
        public int EnrolmentId { get; set; }

        // Who enrolled - Participant
        public int UserId { get; set; }

        // Which event
        public int EventId { get; set; }

        // Which category the Participant picked - e.g. 10km, Senior
        public int CategoryId { get; set; }

        // Part 3: "Pending" = amber, "Confirmed" = green in MVC
        public string Status { get; set; } = "Confirmed";

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        // Navigation properties - don't set these when creating, EF fills them
        public User? User { get; set; }
        public Event? Event { get; set; }
        public Category? Category { get; set; }
        public Result? Result { get; set; }
    }
}