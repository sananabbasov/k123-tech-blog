using System;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;

namespace WebUI.ViewComponents
{
	public class CategoryViewComponent : ViewComponent
	{
		private readonly AppDbContext _context;

        public CategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = _context.Categories.ToList();
			return View("Category", categories);
		}
	}
}

