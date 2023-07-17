using System;
namespace WebUI.StoredProcedures
{
	public class GetTopArticlesByVideoTags
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string SeoUrl { get; set; }
		public string PhotoUrl { get; set; }
        public string TagName { get; set; }
	}
}

