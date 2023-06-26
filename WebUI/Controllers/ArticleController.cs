using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;

        public ArticleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Detail(int id)
        {

            var article = _context.Articles.Include(x=>x.Category).Include(z=>z.User).SingleOrDefault(z=>z.Id == id);
            DetailVM vm = new()
            {
                Article = article
            };
            return View(vm);
        }
    }
}

