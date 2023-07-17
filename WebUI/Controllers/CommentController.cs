using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CommentController(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            comment.UserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            comment.CommentedDate = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();
            var article = _context.Articles.FirstOrDefault(x=>x.Id == comment.ArticleId);
            return Redirect($"/article/detail/{article.Id}/{article.SeoUrl}");
        }
    }
}

