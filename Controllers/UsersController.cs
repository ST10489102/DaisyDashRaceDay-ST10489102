using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) => _context = context;

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized(new { message = "Not logged in" });
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();
            return Ok(user); // <-- FIXED: return whole user, avoids Id error
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();
            return Ok(new { message = "Profile updated" });
        }
    }
}