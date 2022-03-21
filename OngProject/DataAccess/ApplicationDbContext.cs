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


        }

       

      //  public DbSet<Members> Members { get; set; }
    
        public DbSet<Users> Users { get; set; }

    }
}
