using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using DaisyDashRaceDay.MVC.Models;

namespace DaisyDashRaceDay.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public EventsController(IHttpClientFactory factory) => _factory = factory;
        private HttpClient Api() => _factory.CreateClient("Api");

        private class EventApiDto
        {
            [JsonPropertyName("eventId")] public int EventId { get; set; }
            [JsonPropertyName("title")] public string? Title { get; set; }
            [JsonPropertyName("description")] public string? Description { get; set; }
            [JsonPropertyName("location")] public string? Location { get; set; }
            [JsonPropertyName("eventDate")] public DateTime EventDate { get; set; }
            [JsonPropertyName("distance")] public decimal Distance { get; set; }
            [JsonPropertyName("entryFee")] public decimal EntryFee { get; set; }
            [JsonPropertyName("imageUrl")] public string? ImageUrl { get; set; }
            [JsonPropertyName("organiserId")] public int OrganiserId { get; set; }
            [JsonPropertyName("enrolments")] public List<object> Enrolments { get; set; } = new();
        }

        private static EventViewModel ToView(EventApiDto d) => new()
        {
            Id = d.EventId,
            Name = d.Title,
            Description = d.Description,
            Location = d.Location,
            Date = d.EventDate,
            Distance = d.Distance,
            EntryFee = d.EntryFee,
            ImageUrl = d.ImageUrl,
            OrganiserId = d.OrganiserId,
            EnrolmentCount = d.Enrolments?.Count ?? 0,
            Categories = new List<string>()
        };

        private static EventApiDto ToApi(EventViewModel m) => new()
        {
            EventId = m.Id,
            Title = m.Name,
            Description = m.Description,
            Location = m.Location,
            EventDate = m.Date,
            Distance = m.Distance,
            EntryFee = m.EntryFee,
            ImageUrl = m.ImageUrl ?? "",
            OrganiserId = m.OrganiserId,
            Enrolments = new List<object>()
        };

        public async Task<IActionResult> Index()
        {
            var res = await Api().GetAsync("api/Events");
            if (!res.IsSuccessStatusCode) return View(new List<EventViewModel>());
            var dtos = await res.Content.ReadFromJsonAsync<List<EventApiDto>>() ?? new();
            return View(dtos.Select(ToView).ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            var res = await Api().GetAsync($"api/Events/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();
            var dto = await res.Content.ReadFromJsonAsync<EventApiDto>();
            return View(ToView(dto!));
        }

        public IActionResult Create() => View(new EventViewModel { Date = DateTime.Today });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var res = await Api().PostAsJsonAsync("api/Events", ToApi(model));
            if (res.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Failed to create event. API says: " + await res.Content.ReadAsStringAsync());
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await Api().GetAsync($"api/Events/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();
            var dto = await res.Content.ReadFromJsonAsync<EventApiDto>();
            return View(ToView(dto!));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);
            var res = await Api().PutAsJsonAsync($"api/Events/{id}", ToApi(model));
            if (res.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Failed to update. API says: " + await res.Content.ReadAsStringAsync());
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await Api().GetAsync($"api/Events/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();
            var dto = await res.Content.ReadFromJsonAsync<EventApiDto>();
            return View(ToView(dto!));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Api().DeleteAsync($"api/Events/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}