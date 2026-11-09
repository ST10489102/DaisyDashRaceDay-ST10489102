namespace DaisyDashRaceDay.MVC.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime Date { get; set; }
        public decimal Distance { get; set; }
        public decimal EntryFee { get; set; }
        public string? ImageUrl { get; set; }
        public int OrganiserId { get; set; }
        public string? EventType { get; set; }
        public List<string> Categories { get; set; } = new();
        public int EnrolmentCount { get; set; }
    }
}