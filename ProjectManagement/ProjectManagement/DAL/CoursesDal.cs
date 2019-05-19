using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectManagement.DAL
{
    public class CoursesDal : DbContext
    {
            public DbSet<Courses> courses { get; set; }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Courses>().ToTable("Courses");
        }
    }
}