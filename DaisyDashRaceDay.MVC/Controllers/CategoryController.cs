using DaisyDashRaceDay.MVC.Models;
using DaisyDashRaceDay.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaisyDashRaceDay.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApiService _api;
        public CategoryController(ApiService api) => _api = api;

        private bool IsOrganiser() => HttpContext.Session.GetString("Role") == "Organiser";

        public async Task<IActionResult> Index(int eventId)
        {
            var categories = await _api.GetAsync<List<CategoryViewModel>>($"categories/event/{eventId}");
            ViewBag.EventId = eventId;
            return View(categories ?? new List<CategoryViewModel>());
        }

        public IActionResult Create(int eventId)
        {
            if (!IsOrganiser()) return RedirectToAction("Index", "Events");
            return View(new CategoryViewModel { EventId = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!IsOrganiser()) return RedirectToAction("Index", "Events");
            if (!ModelState.IsValid) return View(model);

            var ok = await _api.PostAsync("categories", model);
            if (ok)
            {
                TempData["Success"] = "Category created!";
                return RedirectToAction("Index", new { eventId = model.EventId });
            }
            ModelState.AddModelError("", "Failed to create category.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id, int eventId)
        {
            if (!IsOrganiser()) return RedirectToAction("Index", "Events");
            var category = await _api.GetAsync<CategoryViewModel>($"categories/{id}");
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int eventId)
        {
            if (!IsOrganiser()) return RedirectToAction("Index", "Events");
            await _api.DeleteAsync($"categories/{id}");
            TempData["Success"] = "Category deleted!";
            return RedirectToAction("Index", new { eventId = eventId });
        }
    }
}