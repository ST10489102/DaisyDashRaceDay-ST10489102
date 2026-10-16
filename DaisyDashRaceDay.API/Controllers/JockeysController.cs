using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JockeysController : ControllerBase
    {
        private readonly AppDbContext _context;
        public JockeysController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Jockeys.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Jockey jockey)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can add jockeys" });
            _context.Jockeys.Add(jockey);
            await _context.SaveChangesAsync();
            return Ok(jockey);
        }
    }
}