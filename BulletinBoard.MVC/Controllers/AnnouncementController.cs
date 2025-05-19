using BulletinBoard.MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

public class AnnouncementController : Controller
{
    private readonly HttpClient _httpClient;

    public AnnouncementController()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://bulletinboardapi20250519014155.azurewebsites.net/") // адреса API
        };
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("announcement");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var content = await response.Content.ReadAsStringAsync();
        var announcements = JsonConvert.DeserializeObject<List<AnnouncementDto>>(content);
        ViewBag.Categories = GetCategories();
        return View(announcements);
    }
    public async Task<IActionResult> Filter(string category, string subCategory)
    {
        var response = await _httpClient.GetAsync("announcement");
        var json = await response.Content.ReadAsStringAsync();
        var all = JsonConvert.DeserializeObject<List<AnnouncementDto>>(json);

        if (!string.IsNullOrEmpty(category))
            all = all.Where(a => a.Category == category).ToList();

        if (!string.IsNullOrEmpty(subCategory))
            all = all.Where(a => a.SubCategory == subCategory).ToList();

        return PartialView("_AnnouncementCardsPartial", all);
    }

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = GetCategories();

        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAnnouncementDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        // відправити dto на API
        var response = await _httpClient.PostAsJsonAsync("announcement", dto);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError("", "Помилка при додаванні оголошення.");
        return View(dto);
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var categories = GetCategories();

        ViewData["CategoriesJson"] = System.Text.Json.JsonSerializer.Serialize(categories);
        var response = await _httpClient.GetAsync($"announcement/{id}");

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var announcement = await response.Content.ReadFromJsonAsync<AnnouncementDto>();

        var dto = new UpdateAnnouncementDto
        {
            Id = announcement.Id,
            Title = announcement.Title,
            Description = announcement.Description,
            Category = announcement.Category,
            SubCategory = announcement.SubCategory,
            Status = announcement.Status
        };

        return View(dto);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Update(UpdateAnnouncementDto dto) 
    {
        if (!ModelState.IsValid)
            return View(dto);

        var response = await _httpClient.PutAsJsonAsync($"announcement/{dto.Id}", dto);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError("", "Не вдалося оновити оголошення");
        return View(dto);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Delete(int Id)
    {
        var response = await _httpClient.DeleteAsync($"announcement/{Id}");

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ModelState.AddModelError("", "Не вдалося видалити");
        return View();
    }

    private Dictionary<string, List<string>> GetCategories()
    {
        return new Dictionary<string, List<string>>
        {
            ["Побутова техніка"] = new() { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" },
            ["Комп’ютерна техніка"] = new() { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" },
            ["Смартфони"] = new() { "Android смартфони", "iOS/Apple смартфони" },
            ["Інше"] = new() { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" }
        };
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
