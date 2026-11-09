using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RacesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RacesController(AppDbContext context) => _context = context;

        private bool IsOrganiser()
        {
            return HttpContext.Session.GetString("UserRole") == "Organiser";
        }

        // GET: api/Races - Anyone can view
        [HttpGet]
        public async Task<IActionResult> GetRaces()
        {
            var races = await _context.Races.ToListAsync();
            return Ok(races);
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRace(int id)
        {
            var race = await _context.Races.FindAsync(id);
            if (race == null) return NotFound();
            return Ok(race);
        }

        // POST: api/Races - ONLY Organiser (Session guard - POE requirement)
        [HttpPost]
        public async Task<IActionResult> CreateRace(Race race)
        {
            if (!IsOrganiser())
                return StatusCode(403, new { message = "Only Organiser can create races. Please login as Organiser." });

            // Auto-fix common property names
            _context.Races.Add(race);
            await _context.SaveChangesAsync();
            return Ok(race);
        }

        // PUT: api/Races/5 - ONLY Organiser
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRace(int id, Race updatedRace)
        {
            if (!IsOrganiser())
                return StatusCode(403, new { message = "Only Organiser can update races." });

            var race = await _context.Races.FindAsync(id);
            if (race == null) return NotFound();

            // Update properties - avoids Id mismatch issues
            _context.Entry(race).CurrentValues.SetValues(updatedRace);
            // Keep the original key
            await _context.SaveChangesAsync();
            return Ok(race);
        }

        // DELETE: api/Races/5 - ONLY Organiser
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            if (!IsOrganiser())
                return StatusCode(403, new { message = "Only Organiser can delete races." });

            var race = await _context.Races.FindAsync(id);
            if (race == null) return NotFound();

            _context.Races.Remove(race);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Race deleted" });
        }
    }
}