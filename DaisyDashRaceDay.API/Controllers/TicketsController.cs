using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TicketsController(AppDbContext context) => _context = context;

        // GET my tickets
        [HttpGet]
        public async Task<IActionResult> GetMyTickets()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null) return Unauthorized();
            var tickets = await _context.Tickets.Where(t => t.UserId.ToString() == userId || t.UserId == int.Parse(userId)).ToListAsync();
            return Ok(tickets);
        }

        // POST buy ticket - any logged in user
        [HttpPost]
        public async Task<IActionResult> BuyTicket(Ticket ticket)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null) return StatusCode(401, new { message = "Login first" });

            ticket.UserId = int.Parse(userId);
            ticket.PurchaseDate = DateTime.Now;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }
    }
}