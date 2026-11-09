using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ResultsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");
            if (role == "Participant")
                return Ok(await _context.Results.Where(r => r.UserId == userId).ToListAsync());
            return Ok(await _context.Results.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Result result)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can capture results" });
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}