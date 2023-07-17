using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.StoredProcedures;
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
        public IActionResult Detail(int Id, string seo)
        {

            var article = _context.Articles.Include(x => x.Category).Include(z => z.User).SingleOrDefault(z => z.Id == Id);
            var findNextArticle = _context.Articles.FirstOrDefault(x => x.Id > Id);
            var nextArticle = findNextArticle != null ? findNextArticle : _context.Articles.OrderBy(x => x.Id).FirstOrDefault();
            var findPrevArticle = _context.Articles.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id < Id);
            var prevArticle = findPrevArticle != null ? findPrevArticle : _context.Articles.OrderByDescending(x => x.Id).FirstOrDefault();
            var similarArticle = _context.Articles.Where(x => x.CategoryId == article.CategoryId && x.Id != article.Id).OrderByDescending(x => x.View).Take(2).ToList();
            var popularPost = _context.Articles.FromSqlRaw($"GetPopularPosts").ToList();
            var articleComment = _context.Comments.Include(x => x.User).Where(x=>x.ArticleId == article.Id).ToList();
            var recentComments = _context.Comments.Include(x=>x.Article).OrderByDescending(x=>x.CommentedDate).GroupBy(x=>x.ArticleId).Select(x=>x.First()).Take(3).ToList();
            var videoTags = _context.Set<GetTopArticlesByVideoTags>().FromSqlRaw("GetTopArticlesByVideoTags").ToList();
            DetailVM vm = new()
            {
                Article = article,
                NextArticle = nextArticle,
                PrevArticle = prevArticle,
                SimilarArticle = similarArticle,
                PopularPosts  = popularPost,
                PopularVideoPosts = videoTags,
                Comments = articleComment,
                RecentComments = recentComments
            };

            _context.Database.ExecuteSqlRaw($"AddViews {article.Id}");

            return View(vm);
        }


        public IActionResult Category(string id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.SeoUrl == id);
            var articles = _context.Articles.Where(x => x.CategoryId == category.Id).Include(x => x.Category).Include(z => z.User).ToList();

            ArticleVM article = new()
            {
                Articles = articles,
                Category = category
            };

            return View(article);
        }
    }
}

