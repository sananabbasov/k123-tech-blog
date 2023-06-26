using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var articles = _context.Articles.Include(x=>x.User).Include(x=>x.Category).OrderByDescending(x=>x.Id).ToList();
        var pinnedArticles = _context.Articles.Where(x=>x.IsPinned == true).OrderByDescending(x => x.Id).Take(5).ToList();
        var categories = _context.Categories.ToList();
        HomeVM vm = new()
        {
            Articles = articles,
            Categories = categories,
            PinnedArticles = pinnedArticles
        };
        return View(vm);
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

