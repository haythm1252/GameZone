using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GameZone.Models;

namespace GameZone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGamesServices _GamesService;

    public HomeController(ILogger<HomeController> logger, IGamesServices gamesService)
    {
        _logger = logger;
        _GamesService = gamesService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var games = await _GamesService.GetAllAsync();
        return View(games);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
