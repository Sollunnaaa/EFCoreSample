﻿using EfCoreSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSample
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Recipe> Recipes { get; set; }
    }
}
