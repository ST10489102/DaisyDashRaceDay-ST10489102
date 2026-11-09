using Microsoft.AspNetCore.Mvc;
using DaisyDashRaceDay.MVC.Models;
using System.Text;
using System.Text.Json;

namespace DaisyDashRaceDay.MVC.Controllers
{
    public class EntryController : Controller
    {
        private HttpClient Api()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7161/");
            return client;
        }

        public async Task<IActionResult> Index(int eventId)
        {
            ViewBag.EventId = eventId;
            var res = await Api().GetAsync($"api/Entry?eventId={eventId}");
            if (!res.IsSuccessStatusCode) return View(new List<EntryViewModel>());
            var json = await res.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<List<EntryViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EntryViewModel>();
            return View(list);
        }

        public IActionResult Create(int eventId)
        {
            ViewBag.EventId = eventId;
            return View(new EntryViewModel { EventId = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(EntryViewModel model)
        {
            if (!ModelState.IsValid) { ViewBag.EventId = model.EventId; return View(model); }
            var json = JsonSerializer.Serialize(model);
            var res = await Api().PostAsync("api/Entry", new StringContent(json, Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode) return RedirectToAction("Index", new { eventId = model.EventId });
            ModelState.AddModelError("", "Failed to add entry.");
            ViewBag.EventId = model.EventId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int eventId)
        {
            await Api().DeleteAsync($"api/Entry/{id}");
            return RedirectToAction("Index", new { eventId });
        }
    }
}