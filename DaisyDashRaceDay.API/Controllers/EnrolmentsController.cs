<<<<<<< Updated upstream:DaisyDashRaceDay.API/Controllers/RaceEntriesController.cs
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceEntriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RaceEntriesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.RaceEntries.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(RaceEntry entry)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can link entries" });
            _context.RaceEntries.Add(entry);
            await _context.SaveChangesAsync();
            return Ok(entry);
        }
    }
=======
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DaisyDashRaceDay.API.Data;
using DaisyDashRaceDay.API.Models;

namespace DaisyDashRaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EnrolmentsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Enrolments.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Enrolment entry)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
                return StatusCode(403, new { message = "Only Organiser can link entries" });
            _context.Enrolments.Add(entry);
            await _context.SaveChangesAsync();
            return Ok(entry);
        }
    }
>>>>>>> Stashed changes:DaisyDashRaceDay.API/Controllers/EnrolmentsController.cs
}