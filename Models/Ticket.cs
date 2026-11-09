using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int RaceId { get; set; }
        public string SeatNumber { get; set; } = "";
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}