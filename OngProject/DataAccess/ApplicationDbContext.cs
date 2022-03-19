using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) { }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedNews(builder);
        }

        public void SeedNews(ModelBuilder modelBuilder)
        {
            for(int i=1;i<11;i++)
                {
                modelBuilder.Entity<News>().HasData(
                    new News
                    {
                        Id = i,
                        Name = "News" + i,
                        Content = "Content from News" + i,
                        Image = "Image from News" + i,
                        DateModified = DateTime.Now
                    }) ;
                }
        }


        //public DbSet<Members> Members { get; set; }
        //public DbSet<News> News { get; set; }
    }
}
