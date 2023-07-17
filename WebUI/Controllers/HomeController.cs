using System.Diagnostics;
using System.Linq;
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
    // localhost:5431/home/index?page=2
    public IActionResult Index(int page=1)
    {
        int productCount = _context.Articles.Count();
        ViewBag.PageCount = productCount % 3 == 0 ? productCount / 3 : productCount / 3 + 1;
        ViewBag.NextPage = page + 1;
        ViewBag.PrevPage = page - 1;
        ViewBag.CurrentPage = page;
        if (ViewBag.PrevPage==null || page - 1 <= 0)
        {
            ViewBag.PrevPage = 1;
        }
        int skipPage = (page * 3)-3;
        var articles = _context.Articles.Include(x=>x.User).Include(x=>x.Category).OrderByDescending(x=>x.Id).Skip(skipPage).Take(3).ToList();
        if (!articles.Any())
        {
            return RedirectToAction("NotFound");
        }
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

    public IActionResult NotFound()
    {
        return View();
    }
}

