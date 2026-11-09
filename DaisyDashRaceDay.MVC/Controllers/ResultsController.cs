using Microsoft.AspNetCore.Mvc;
using DaisyDashRaceDay.MVC.Models;
using System.Text;
using System.Text.Json;

namespace DaisyDashRaceDay.MVC.Controllers
{
    public class ResultController : Controller
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
            var res = await Api().GetAsync($"api/Results?eventId={eventId}");
            if (!res.IsSuccessStatusCode) return View(new List<ResultsViewModel>());
            var json = await res.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<List<ResultsViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ResultsViewModel>();
            return View(list);
        }

        public IActionResult Create(int eventId)
        {
            ViewBag.EventId = eventId;
            return View(new ResultsViewModel { EventId = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResultsViewModel model)
        {
            if (!ModelState.IsValid) { ViewBag.EventId = model.EventId; return View(model); }
            var json = JsonSerializer.Serialize(model);
            var res = await Api().PostAsync("api/Results", new StringContent(json, Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode) return RedirectToAction("Index", new { eventId = model.EventId });
            ModelState.AddModelError("", "Failed to add result.");
            ViewBag.EventId = model.EventId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int eventId)
        {
            await Api().DeleteAsync($"api/Results/{id}");
            return RedirectToAction("Index", new { eventId });
        }
    }
}