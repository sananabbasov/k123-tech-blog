using System;
using WebUI.Models;

namespace WebUI.ViewModels
{
	public class ArticleVM
	{
		public List<Article> Articles { get; set; }
		public Category Category { get; set; }
	}
}

