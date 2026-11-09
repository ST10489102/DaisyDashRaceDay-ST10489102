<<<<<<< Updated upstream
﻿namespace DaisyDashRaceDay.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = ""; // e.g. Under 20, Senior, 10km, 21km
        public string? Description { get; set; }
        public int EventId { get; set; } // FK to Race/Event
        public Race? Event { get; set; }
    }
=======
﻿using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        // e.g. "5km", "10km", "Senior", "U18"
        [Required]
        public string Name { get; set; } = "";

        public string? Description { get; set; }

        // Which event this category belongs to - Organiser sets this
        public int EventId { get; set; }

        // Navigation - don't set this when creating, EF fills it
        public Event? Event { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
>>>>>>> Stashed changes
}