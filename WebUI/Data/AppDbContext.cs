﻿using System;
using WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUI.StoredProcedures;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Data
{
	public class AppDbContext : IdentityDbContext<User>
    {
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<ArticleTag> ArticleTags { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Comment> Comments { get; set; }



		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.Entity<User>().ToTable("Users");
			builder.Entity<IdentityRole>().ToTable("Roles");
			builder.Entity<GetTopArticlesByVideoTags>().ToView(null);
        }

    }
}

