using System;
using WebUI.Models;

namespace WebUI.ViewModels
{
	public class HomeVM
	{
		public List<Article> Articles { get; set; }
		public List<Article> PinnedArticles { get; set; }
        public List<Category> Categories { get; set; }
	}
}

