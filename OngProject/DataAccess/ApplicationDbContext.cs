using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


            // call the method that loads seed data for database testing
            // llamar a los metodos seed en orden segun las relaciones entre entidades
            builder.SeedCategories();
            builder.SeedActivities();
            builder.SeedNews();
            builder.SeedComentaries();
            builder.SeedMembers();
            builder.SeedRoles();
            builder.SeedUsers();
        }



        //  public DbSet<Members> Members { get; set; }

        public DbSet<Activities> Activities { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comentaries> Comentaries { get; set; }

    }
}
