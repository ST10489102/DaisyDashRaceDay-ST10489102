namespace DaisyDashRaceDay.MVC.Models;

public class CategoryViewModel
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string Name { get; set; } = ""; // e.g. Under 20, Senior, 10km, 21km
    public string? Description { get; set; }
}