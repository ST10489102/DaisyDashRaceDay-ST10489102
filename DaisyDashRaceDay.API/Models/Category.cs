namespace DaisyDashRaceDay.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = ""; // e.g. Under 20, Senior, 10km, 21km
        public string? Description { get; set; }
        public int EventId { get; set; } // FK to Race/Event
        public Race? Event { get; set; }
    }
}