using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Events - Anyone can view
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Enrolments)
                .ToListAsync();
            return Ok(events);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var ev = await _context.Events
                .Include(e => e.Enrolments)
                .FirstOrDefaultAsync(e => e.EventId == id);
            if (ev == null) return NotFound(new { message = "Event not found" });
            return Ok(ev);
        }

        // POST: api/Events - Organiser only
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event ev)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can create events" });

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = ev.EventId }, ev);
        }

        // PUT: api/Events/5 - Organiser only
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updated)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can update events" });

            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound(new { message = "Event not found" });

            ev.Title = updated.Title;
            ev.Description = updated.Description;
            ev.Location = updated.Location;
            ev.EventDate = updated.EventDate;
            ev.EntryFee = updated.EntryFee;
            ev.ImageUrl = updated.ImageUrl;

            await _context.SaveChangesAsync();
            return Ok(ev);
        }

        // DELETE: api/Events/5 - Organiser only
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can delete events" });

            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound(new { message = "Event not found" });

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Event deleted" });
        }
    }
}