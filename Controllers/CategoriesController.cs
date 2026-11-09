using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Categories.ToListAsync());

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId) =>
            Ok(await _context.Categories.Where(c => c.EventId == eventId).ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Category cat)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can add categories" });
            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();
            return Ok(cat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can delete" });
            var c = await _context.Categories.FindAsync(id);
            if (c == null) return NotFound();
            _context.Categories.Remove(c);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Category deleted" });
        }
    }
}