﻿using Microsoft.EntityFrameworkCore;
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
            
        }


        //public DbSet<Members> Members { get; set; }
        //public DbSet<News> News { get; set; }
    }
}
