using System.Text;
using System.Text.Json;
using DaisyDashRaceDay.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaisyDashRaceDay.MVC.Controllers;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _factory;
    public AccountController(IHttpClientFactory factory) => _factory = factory;

    private HttpClient Api() => _factory.CreateClient("RaceDayApi");

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    // POST: /Account/Register -> POST /api/Auth/register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var json = JsonSerializer.Serialize(new
        {
            fullName = vm.FullName,
            email = vm.Email,
            password = vm.Password,
            role = vm.Role
        });

        var res = await Api().PostAsync("api/Auth/register",
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (res.IsSuccessStatusCode)
        {
            TempData["Success"] = "Account created. Please log in.";
            return RedirectToAction("Login");
        }

        ModelState.AddModelError("", "Registration failed. Email may already be used.");
        return View(vm);
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    // POST: /Account/Login -> POST /api/Auth/login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var json = JsonSerializer.Serialize(new { email = vm.Email, password = vm.Password });
        var res = await Api().PostAsync("api/Auth/login",
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (!res.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(vm);
        }

        var body = await res.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(body);
        var root = doc.RootElement;

        // API may return { id, fullName, email, role } or { user: {...} }
        var user = root.TryGetProperty("user", out var u) ? u : root;

        string Get(string name) => user.TryGetProperty(name, out var v) ? v.ToString() : "";

        // Handle both "id" and "userId"
        var id = Get("id");
        if (string.IsNullOrEmpty(id)) id = Get("userId");

        HttpContext.Session.SetString("UserId", id);
        HttpContext.Session.SetString("Role", Get("role"));
        HttpContext.Session.SetString("FullName", Get("fullName"));
        HttpContext.Session.SetString("Email", Get("email"));

        // Role-based redirect - required by brief
        if (Get("role").Equals("Organiser", StringComparison.OrdinalIgnoreCase))
            return RedirectToAction("Index", "Events");
        return RedirectToAction("Index", "Home");
    }

    // GET: /Account/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    // GET: /Account/Profile -> GET /api/Users/{id}
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login");

        var res = await Api().GetAsync($"api/Users/{userId}");
        if (!res.IsSuccessStatusCode) return RedirectToAction("Login");

        var vm = await res.Content.ReadFromJsonAsync<ProfileViewModel>()
                 ?? new ProfileViewModel { Id = int.Parse(userId) };
        return View(vm);
    }

    // POST: /Account/Profile -> PUT /api/Users/{id}
    [HttpPost]
    public async Task<IActionResult> Profile(ProfileViewModel vm)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login");

        var json = JsonSerializer.Serialize(vm);
        var res = await Api().PutAsync($"api/Users/{userId}",
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (res.IsSuccessStatusCode)
        {
            TempData["Success"] = "Profile updated.";
            HttpContext.Session.SetString("FullName", vm.FullName);
            return RedirectToAction("Profile");
        }

        ModelState.AddModelError("", "Could not update profile.");
        return View(vm);
    }
}