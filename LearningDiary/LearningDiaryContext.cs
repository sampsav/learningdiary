﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningDiary
{
    public class LearningDiaryContext : DbContext 
    {

        public LearningDiaryContext()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbConfigStr = @"Server=DESKTOP-7BQQ30N\MSSQLSERVER2;Database=LearningDiary;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(dbConfigStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Topic>()
                .Property(e => e.TimeSpent)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Task> Tasks { get; set; }
        

    }
    
   
}
