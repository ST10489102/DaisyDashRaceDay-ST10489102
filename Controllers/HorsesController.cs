using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorsesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public HorsesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Horses.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var h = await _context.Horses.FindAsync(id);
            if (h == null) return NotFound();
            return Ok(h);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Horse horse)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can add horses" });
            _context.Horses.Add(horse);
            await _context.SaveChangesAsync();
            return Ok(horse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can delete" });
            var h = await _context.Horses.FindAsync(id);
            if (h == null) return NotFound();
            _context.Horses.Remove(h);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Horse deleted" });
        }
    }
}