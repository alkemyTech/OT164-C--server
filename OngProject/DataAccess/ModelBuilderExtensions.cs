﻿using Microsoft.EntityFrameworkCore;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void SeedCategories(this ModelBuilder modelBuilder)
        {
            for (int i = 1; i < 6; i++)
            {
                modelBuilder.Entity<Categories>().HasData(
                    new Categories 
                    {
                        Id = i,
                        Name = "Category" + i,
                        Description = "Category description " + i,
                        Image = " Image from category " + i,
                        DateModified = DateTime.Now
                    });
            }
        }

    }
}
