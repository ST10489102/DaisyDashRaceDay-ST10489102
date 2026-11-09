<<<<<<< Updated upstream
﻿namespace DaisyDashRaceDay.API.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int EnrolmentId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public TimeSpan FinishTime { get; set; }
        public int Position { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.Now;
    }
=======
﻿namespace DaisyDashRaceDay.API.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        // Which enrolment this result is for - 1 enrolment = 1 result max
        public int EnrolmentId { get; set; }

        // Who got this result - Participant, for My Results filtering
        public int UserId { get; set; }

        // Which event - for showing event name on My Results page
        public int EventId { get; set; }

        // e.g. 00:45:30 - TimeSpan works in SQL + in MVC views
        public TimeSpan FinishTime { get; set; }

        // e.g. 1, 2, 3
        public int Position { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        // Navigation - don't set these when creating, EF fills them
        public Enrolment? Enrolment { get; set; }
        public User? User { get; set; }
        public Event? Event { get; set; }
    }
>>>>>>> Stashed changes
}