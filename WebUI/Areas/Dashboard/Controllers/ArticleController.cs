using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin, Redaktor")]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public ArticleController(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var currentUserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var articles = _context.Articles.Include(x => x.Category).Where(x => x.UserId == currentUserId).ToList();
            return View(articles);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            article.CreatedDate = DateTime.Now;
            article.UpdatedDate = DateTime.Now;
            article.UserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _context.Articles.Add(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var currentUserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var article = _context.Articles.SingleOrDefault(z => z.Id == id);
            if (article.UserId != currentUserId)
            {
                return NotFound();
            }
            var categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(Article article)
        {
            article.UserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            article.UpdatedDate = DateTime.Now;
            _context.Articles.Update(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

