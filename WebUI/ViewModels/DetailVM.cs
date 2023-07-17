using System;
using WebUI.DTOs;
using WebUI.Models;
using WebUI.StoredProcedures;

namespace WebUI.ViewModels
{
	public class DetailVM
	{
		public Article Article { get; set; }
		public Article PrevArticle { get; set; }
		public Article NextArticle { get; set; }
		public List<Article> SimilarArticle { get; set; }
		public List<Article> PopularPosts { get; set; }
        public List<GetTopArticlesByVideoTags> PopularVideoPosts { get; set; }
		public List<Comment> Comments { get; set; }
		public List<Comment> RecentComments { get; set; }
    }
}

